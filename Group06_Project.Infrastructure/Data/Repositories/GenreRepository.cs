using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class GenreRepository : RepositoryBase<Genre, int>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<GenreOptions> GetAllGenresHomeModel()
    {
        return DbSet.Select(g => new GenreOptions
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();
    }
}