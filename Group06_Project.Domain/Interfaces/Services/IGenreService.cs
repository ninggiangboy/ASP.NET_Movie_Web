using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IGenreService
{
    IEnumerable<SelectListItem> GetGenresHomeList();
    IEnumerable<HomeItem> GetGenreHomeItems();
}