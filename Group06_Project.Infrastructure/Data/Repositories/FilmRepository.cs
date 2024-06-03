using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class FilmRepository : RepositoryBase<Film, int>, IFilmRepository
{
    public FilmRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<Film> GetList(string userId)
    {
        return DbContext.Users
            .Include(x => x.FavoriteFilms)
            .Where(x => x.Id == userId)?
            .SelectMany(u => u.FavoriteFilms).ToList() ?? new List<Film>();
    } 
}