using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Group06_Project.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Domain.Entities;

public class Film
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(100)] [Unicode] public string Title { get; set; } = null!;
    [StringLength(100)] [Unicode] public string? OtherTitle { get; set; }
    [StringLength(100)] public string Slug { get; set; } = null!;
    [Unicode] public string? Description { get; set; }
    [StringLength(255)] public string? TrailerUrl { get; set; }
    [StringLength(255)] public string? PosterUrl { get; set; }
    [StringLength(255)] public string? ThumbnailUrl { get; set; }
    public int? Duration { get; set; }
    [Precision(18, 2)] public decimal? AverageRating { get; set; }
    public int? TotalEpisode { get; set; }
    public int? DurationPerEpisode { get; set; }
    [StringLength(255)] public string? VideoUrl { get; set; }
    public FilmType Type { get; set; } = FilmType.Movie;
    public string? Actor { get; set; }
    public string? Director { get; set; }
    public int TotalView { get; set; } = 0;
    public int? ReleaseYear { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? CountryId { get; set; }
    public virtual Country? Country { get; set; }
    public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();
    public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    public virtual ICollection<User> Followers { get; set; } = new HashSet<User>();
}