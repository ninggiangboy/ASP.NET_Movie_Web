namespace Group06_Project.Domain.Models;

public class FilmItemDetail
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? OtherTitle { get; set; }
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? PosterUrl { get; set; }
    public int? Duration { get; set; }
    public decimal? AverageRating { get; set; }
    public int? TotalEpisode { get; set; }
    public int? DurationPerEpisode { get; set; }
    public string? Actor { get; set; }
    public string? Director { get; set; }
    public int TotalView { get; set; } = 0;
    public int? ReleaseYear { get; set; }
    public SelectOption? Country { get; set; }
    public IEnumerable<EpisodeItem> Episodes { get; set; } = new HashSet<EpisodeItem>();
    public IEnumerable<SelectOption> Genres { get; set; } = new HashSet<SelectOption>();
}