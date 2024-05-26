using Microsoft.AspNetCore.Identity;

namespace Group06_Project.Domain.Entities;

public class User : IdentityUser
{
    public virtual ICollection<Film> FavoriteFilms { get; set; } = new HashSet<Film>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
}