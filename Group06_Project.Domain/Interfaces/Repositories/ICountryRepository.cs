using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface ICountryRepository : IRepositoryBase<Country, int>
{
    IEnumerable<SelectListItem> GetAllCountryOptions();
    IEnumerable<HomeItem> GetAllCountryHomeItems();
}