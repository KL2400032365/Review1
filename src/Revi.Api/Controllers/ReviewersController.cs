using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ReviewersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IAllocationService _allocation;

    public ReviewersController(ApplicationDbContext db, IAllocationService allocation)
    {
        _db = db;
        _allocation = allocation;
    }

    /// <summary>
    /// Run peer allocation for an assignment.
    /// </summary>
    [HttpPost("{assignmentId}/allocate")]
    public async Task<IActionResult> RunAllocation(int assignmentId, [FromBody] AllocationRequest req)
    {
        var assignment = await _db.Assignments.FindAsync(assignmentId);
        if (assignment == null) return NotFound("Assignment not found");

        var allocations = await _allocation.AllocateReviewersAsync(
            assignmentId,
            req.ReviewsPerSubmission ?? 2,
            preventSelfReview: true);

        // Store allocation records (optional: for audit/tracking)
        foreach (var (reviewerId, submissionId) in allocations)
        {
            // You could create an AllocationRecord here to track decisions
            // For now, we just return the allocations
        }

        return Ok(new { allocated = allocations.Count, allocations });
    }

    /// <summary>
    /// Get reviews assigned to a user (as reviewer).
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReviewsForReviewer(string userId)
    {
        var reviews = await _db.Reviews
            .Where(r => r.ReviewerId == userId)
            .Include(r => r.Submission)
            .ToListAsync();

        return Ok(reviews);
    }

    /// <summary>
    /// Get reviews for a submission (for instructor overview).
    /// </summary>
    [HttpGet("submission/{submissionId}")]
    public async Task<IActionResult> GetReviewsForSubmission(int submissionId)
    {
        var reviews = await _db.Reviews
            .Where(r => r.SubmissionId == submissionId)
            .ToListAsync();

        return Ok(reviews);
    }

    public class AllocationRequest
    {
        public int? ReviewsPerSubmission { get; set; }
        public bool PreventSelfReview { get; set; } = true;
    }
}
