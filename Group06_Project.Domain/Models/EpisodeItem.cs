namespace Group06_Project.Domain.Models;

public class EpisodeItem
{
    public int Id { get; init; }
    public int Number { get; set; }
    public string Title { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public string? VideoUrl { get; set; }
    public int? Duration { get; set; }
}