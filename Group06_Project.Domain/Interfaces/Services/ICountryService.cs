using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Domain.Interfaces.Services;

public interface ICountryService
{
    IEnumerable<SelectListItem> GetCountryOptionsList();
    IEnumerable<HomeItem> GetCountryHomeItems();
}