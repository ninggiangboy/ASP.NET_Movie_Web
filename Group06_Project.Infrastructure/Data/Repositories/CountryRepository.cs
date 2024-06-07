using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class CountryRepository : RepositoryBase<Country, int>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<SelectListItem> GetAllCountryOptions()
    {
        return DbSet.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
    }

    public IEnumerable<HomeItem> GetAllCountryHomeItems()
    {
        return DbSet.Select(c => new HomeItem
        {
            Id = c.Id,
            Name = c.Name,
            Image = c.Image,
            TotalItem = c.Films.Count
        }).ToList();
    }
}