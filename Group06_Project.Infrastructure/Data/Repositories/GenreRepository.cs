using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class GenreRepository : RepositoryBase<Genre, int>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<SelectListItem> GetAllGenresHomeModel()
    {
        return DbSet.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Name
        }).ToList();
    }

    public IEnumerable<HomeItem> GetAllGenreHomeItems()
    {
        return DbSet.Select(g => new HomeItem
        {
            Id = g.Id,
            Name = g.Name,
            Image = g.Image,
            TotalItem = g.Films.Count
        }).ToList();
    }
}