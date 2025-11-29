using System;
using System.ComponentModel.DataAnnotations;

public class Submission
{
    [Key]
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public string FilePath { get; set; } = string.Empty; // path or link
    public int Version { get; set; }
}
