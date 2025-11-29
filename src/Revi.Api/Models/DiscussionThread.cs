using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DiscussionThread
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<Comment> Comments { get; set; } = new();
}
