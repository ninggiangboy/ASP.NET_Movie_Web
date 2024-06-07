namespace Group06_Project.Domain.Models;

public class CommentItem
{
    public string Content { get; set; } = null!;
    public DateTime Time { get; set; }
    public string UserName { get; set; } = null!;
}