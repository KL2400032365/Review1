using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public AssignmentsController(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.Assignments.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var a = await _db.Assignments.FindAsync(id);
        if (a == null) return NotFound();
        return Ok(a);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId) => Ok(await _db.Assignments.Where(a => a.CourseId == courseId).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssignmentRequest req)
    {
        var assignment = new Assignment
        {
            Title = req.Title,
            Description = req.Description,
            CourseId = req.CourseId,
            DueDate = req.DueDate,
            AnonymousMode = req.AnonymousMode ?? false,
            ReviewsPerSubmission = req.ReviewsPerSubmission ?? 2
        };

        _db.Assignments.Add(assignment);
        await _db.SaveChangesAsync();

        // Optionally add rubric items if provided
        if (req.RubricItems?.Count > 0)
        {
            foreach (var item in req.RubricItems)
            {
                _db.RubricItems.Add(new RubricItem
                {
                    AssignmentId = assignment.Id,
                    Criteria = item.Criteria,
                    MaxPoints = item.MaxPoints,
                    Weight = item.Weight
                });
            }
            await _db.SaveChangesAsync();
        }

        return CreatedAtAction(nameof(Get), new { id = assignment.Id }, assignment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateAssignmentRequest req)
    {
        var existing = await _db.Assignments.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Title = req.Title ?? existing.Title;
        existing.Description = req.Description ?? existing.Description;
        existing.DueDate = req.DueDate ?? existing.DueDate;
        existing.AnonymousMode = req.AnonymousMode ?? existing.AnonymousMode;
        existing.ReviewsPerSubmission = req.ReviewsPerSubmission ?? existing.ReviewsPerSubmission;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _db.Assignments.FindAsync(id);
        if (existing == null) return NotFound();
        _db.Assignments.Remove(existing);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/status")]
    public async Task<IActionResult> GetStatus(int id)
    {
        var assignment = await _db.Assignments.FindAsync(id);
        if (assignment == null) return NotFound();

        var submissionCount = await _db.Submissions.CountAsync(s => s.AssignmentId == id);
        var reviewCount = await _db.Reviews.CountAsync(r => r.Submission.AssignmentId == id);

        return Ok(new
        {
            id = assignment.Id,
            title = assignment.Title,
            dueDate = assignment.DueDate,
            submissions = submissionCount,
            reviews = reviewCount,
            status = assignment.DueDate < DateTime.UtcNow ? "closed" : "open"
        });
    }

    public class CreateAssignmentRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? AnonymousMode { get; set; }
        public int? ReviewsPerSubmission { get; set; }
        public List<RubricItemRequest>? RubricItems { get; set; }
    }

    public class RubricItemRequest
    {
        public string Criteria { get; set; } = string.Empty;
        public int MaxPoints { get; set; }
        public decimal Weight { get; set; }
    }
}
