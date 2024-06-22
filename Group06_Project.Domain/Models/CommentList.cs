namespace Group06_Project.Domain.Models;

public class CommentList
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int FilmId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime Time { get; set; }
}