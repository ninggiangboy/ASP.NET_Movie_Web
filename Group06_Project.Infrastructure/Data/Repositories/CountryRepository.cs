using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class CountryRepository : RepositoryBase<Country, int>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}