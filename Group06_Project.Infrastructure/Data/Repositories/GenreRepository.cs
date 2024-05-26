using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class GenreRepository : RepositoryBase<Genre, int>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}