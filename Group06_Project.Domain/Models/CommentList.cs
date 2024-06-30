namespace Group06_Project.Domain.Models;

public class CommentList
{
    public int Id { get; init; }
    public string UserId { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public int FilmId { get; init; }
    public string FilmTitle { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime Time { get; init; }
}