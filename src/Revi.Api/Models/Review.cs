using System;
using System.ComponentModel.DataAnnotations;

public class Review
{
    [Key]
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public string ReviewerId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime ReviewedAt { get; set; }
}
