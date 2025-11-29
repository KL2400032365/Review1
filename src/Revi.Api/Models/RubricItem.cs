using System.ComponentModel.DataAnnotations;

public class RubricItem
{
    [Key]
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public string Criterion { get; set; } = string.Empty;
    public int MaxScore { get; set; }
}
