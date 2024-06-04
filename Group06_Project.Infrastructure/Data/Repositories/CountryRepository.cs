using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class CountryRepository : RepositoryBase<Country, int>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<SelectOption> GetAllCountryOptions()
    {
        return DbSet.Select(c => new SelectOption
        {
            Value = c.Id,
            Label = c.Name
        }).ToList();
    }
}