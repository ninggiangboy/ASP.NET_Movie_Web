using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IGenreRepository : IRepositoryBase<Genre, int>
{
    IEnumerable<SelectListItem> GetAllGenresHomeModel();
    public IEnumerable<HomeItem> GetAllGenreHomeItems();
}