using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface ICountryRepository : IRepositoryBase<Country, int>
{
    IEnumerable<CountryOptions> GetAllCountryOptions();
}