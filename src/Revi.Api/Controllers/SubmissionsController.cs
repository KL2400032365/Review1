using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public SubmissionsController(ApplicationDbContext db) => _db = db;

    [HttpGet("assignment/{assignmentId}")]
    public async Task<IActionResult> GetByAssignment(int assignmentId) => Ok(await _db.Submissions.Where(s => s.AssignmentId == assignmentId).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Submission model)
    {
        model.SubmittedAt = DateTime.UtcNow;
        model.Version = (await _db.Submissions.CountAsync(s => s.AssignmentId == model.AssignmentId && s.UserId == model.UserId)) + 1;
        _db.Submissions.Add(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByAssignment), new { assignmentId = model.AssignmentId }, model);
    }
}
