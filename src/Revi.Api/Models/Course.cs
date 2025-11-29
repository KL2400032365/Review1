using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Course
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public List<Batch> Batches { get; set; } = new();
}
