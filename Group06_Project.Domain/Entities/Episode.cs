using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Domain.Entities;

public class Episode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public int Number { get; set; }

    [StringLength(100)] [Unicode] public string Title { get; set; } = null!;
    [StringLength(255)] public string? ThumbnailUrl { get; set; }
    [StringLength(255)] public string VideoUrl { get; set; } = null!;
    public int View { get; set; } = 0;
    public int? Duration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int MovieId { get; set; }
    public virtual Film Movie { get; set; } = null!;
}