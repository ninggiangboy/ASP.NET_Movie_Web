using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group06_Project.Domain.Entities;

public class FilmUser
{
    [Key]
    [Column(Order = 1)]
    public string FollowersId { get; set; } 
    [Key]
    [Column(Order = 2)]
    public int FavoriteFilmsId { get; set; }
    public virtual User Follower { get; set; }
    public virtual Film FavoriteFilm { get; set; }
}