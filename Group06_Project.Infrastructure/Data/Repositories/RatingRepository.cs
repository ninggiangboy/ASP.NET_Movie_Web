using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class RatingRepository : RepositoryBase<Rating, int>, IRatingRepository
{
    public RatingRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}