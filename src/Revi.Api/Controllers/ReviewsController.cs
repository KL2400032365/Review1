using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public ReviewsController(ApplicationDbContext db) => _db = db;

    [HttpGet("submission/{submissionId}")]
    public async Task<IActionResult> GetBySubmission(int submissionId) => Ok(await _db.Reviews.Where(r => r.SubmissionId == submissionId).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Review model)
    {
        model.ReviewedAt = DateTime.UtcNow;
        _db.Reviews.Add(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBySubmission), new { submissionId = model.SubmissionId }, model);
    }
}
