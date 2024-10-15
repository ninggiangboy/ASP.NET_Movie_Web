namespace Group06_Project.Domain.Models;

public class FilmListExport
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
    public string? Actor { get; set; }
    public string? Director { get; set; }
    public int TotalView { get; set; } = 0;
    public int? ReleaseYear { get; set; }
}