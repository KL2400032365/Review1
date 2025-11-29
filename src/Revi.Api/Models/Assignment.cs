using System;
using System.ComponentModel.DataAnnotations;

public class Assignment
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Marks { get; set; }
    public int CourseId { get; set; }
    public bool AnonymousMode { get; set; }
    public int ReviewsPerSubmission { get; set; } = 2;
}
