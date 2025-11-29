using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IAllocationService
{
    Task<List<(string reviewerId, int submissionId)>> AllocateReviewersAsync(
        int assignmentId, 
        int reviewsPerSubmission, 
        bool preventSelfReview = true);
}

public class AllocationService : IAllocationService
{
    private readonly ApplicationDbContext _db;

    public AllocationService(ApplicationDbContext db) => _db = db;

    /// <summary>
    /// Allocate reviewers to submissions for an assignment using random sampling.
    /// Prevents self-review if requested, respects submission/reviewer counts.
    /// </summary>
    public async Task<List<(string reviewerId, int submissionId)>> AllocateReviewersAsync(
        int assignmentId,
        int reviewsPerSubmission,
        bool preventSelfReview = true)
    {
        var submissions = await _db.Submissions
            .Where(s => s.AssignmentId == assignmentId)
            .ToListAsync();

        var enrollments = await _db.Enrollments
            .Where(e => submissions.Select(s => s.CourseId).Distinct().Contains(e.CourseId))
            .ToListAsync();

        var studentIds = enrollments.Where(e => !e.IsTeacher).Select(e => e.UserId).Distinct().ToList();

        var allocations = new List<(string, int)>();

        foreach (var submission in submissions)
        {
            var availableReviewers = studentIds.AsEnumerable();

            // Prevent self-review if enabled
            if (preventSelfReview)
            {
                availableReviewers = availableReviewers.Where(id => id != submission.UserId);
            }

            // Random sampling for reviewer selection
            var selected = availableReviewers
                .OrderBy(_ => Guid.NewGuid())
                .Take(reviewsPerSubmission)
                .ToList();

            foreach (var reviewerId in selected)
            {
                allocations.Add((reviewerId, submission.Id));
            }
        }

        return allocations;
    }
}
