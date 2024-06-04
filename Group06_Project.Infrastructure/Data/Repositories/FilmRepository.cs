using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class FilmRepository : RepositoryBase<Film, int>, IFilmRepository
{
    public FilmRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public Page<FilmHomeModel> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>> predicate)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var data = DbSet.Include(f => f.Genres).Select(f => new FilmHomeModel
        {
            Id = f.Id,
            Title = f.Title,
            PosterUrl = f.PosterUrl ?? "",
            AverageRating = f.AverageRating ?? 0,
            TotalView = f.TotalView,
            Genres = f.Genres.Select(g => g.Name).ToList()
        }).Skip(skip).Take(pageRequest.Size).OrderBy(pageRequest.Sort ?? "Id desc");


        var totalElement = data.Count();
        return new Page<FilmHomeModel>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<FilmHomeModel>() : data.ToList()
        };
    }

    public ICollection<FilmHomeModel> GetFavoriteFilms(string userId)
    {
        return DbContext.Users.Include(u => u.FavoriteFilms).FirstOrDefault(u => u.Id == userId).FavoriteFilms.
            Select(f => new FilmHomeModel
            {
                Id = f.Id,
                Title = f.Title,
                PosterUrl = f.PosterUrl ?? "",
                AverageRating = f.AverageRating ?? 0,
                TotalView = f.TotalView,
                Genres = f.Genres.Select(g => g.Name).ToList()
            }).ToList(); ;
    }
}