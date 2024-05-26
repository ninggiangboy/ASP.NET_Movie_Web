using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class FilmRepository : RepositoryBase<Film, int>, IFilmRepository
{
    public FilmRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}