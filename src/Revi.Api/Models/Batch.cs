using System.ComponentModel.DataAnnotations;

public class Batch
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g., "Batch A"
    public int CourseId { get; set; }
    public Course? Course { get; set; }
}
