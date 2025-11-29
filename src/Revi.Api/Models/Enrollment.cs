using System.ComponentModel.DataAnnotations;

public class Enrollment
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty; // Identity user id
    public int CourseId { get; set; }
    public int? BatchId { get; set; }
    public bool IsTeacher { get; set; }
}
