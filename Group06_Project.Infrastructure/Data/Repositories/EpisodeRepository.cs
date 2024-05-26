using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class EpisodeRepository : RepositoryBase<Episode, int>, IEpisodeRepository
{
    public EpisodeRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}