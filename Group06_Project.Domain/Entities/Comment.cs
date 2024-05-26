using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Domain.Entities;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public string UserId { get; set; } = null!;
    public int? FilmId { get; set; }
    [Unicode] public string Content { get; set; } = null!;
    public DateTime? Time { get; set; } = DateTime.Now;
    public virtual Film Film { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}