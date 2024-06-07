using System.ComponentModel.DataAnnotations;

namespace Group06_Project.Domain.Entities;

public class Rating
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    [StringLength(450)] public string UserId { get; set; } = null!;
    public int Rate { get; set; }
    public virtual Film Film { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}