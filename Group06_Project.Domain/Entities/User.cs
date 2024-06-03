using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Domain.Entities;

public class User : IdentityUser
{
    [Precision(18, 2)] public decimal Balance { get; set; } = 0;
    public virtual ICollection<Film> FavoriteFilms { get; set; } = new HashSet<Film>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
}