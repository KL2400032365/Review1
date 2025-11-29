using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DiscussionsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public DiscussionsController(ApplicationDbContext db) => _db = db;

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId) => Ok(await _db.Discussions.Where(d => d.CourseId == courseId).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(DiscussionThread model)
    {
        model.CreatedAt = DateTime.UtcNow;
        _db.Discussions.Add(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByCourse), new { courseId = model.CourseId }, model);
    }
}
