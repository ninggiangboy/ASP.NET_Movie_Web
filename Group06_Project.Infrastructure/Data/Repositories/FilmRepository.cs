using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class FilmRepository : RepositoryBase<Film, int>, IFilmRepository
{
    public FilmRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public Page<FilmItemList> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>>? predicate)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var rawData = DbSet
            .Include(f => f.Genres)
            .Where(predicate ?? (_ => true));
        var totalElement = rawData.Count();
        var data = rawData
            .OrderBy(pageRequest.Sort ?? "Id desc")
            .Skip(skip).Take(pageRequest.Size)
            .Select(f => new FilmItemList
            {
                Id = f.Id,
                Title = f.Title,
                PosterUrl = f.PosterUrl ?? "",
                AverageRating = f.AverageRating ?? 0,
                TotalView = f.TotalView,
                Genres = f.Genres.Select(g => new SelectOption
                {
                    Value = g.Id,
                    Label = g.Name
                })
            });
        return new Page<FilmItemList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<FilmItemList>() : data.ToList()
        };
    }

    public bool ExistsById(int id)
    {
        return DbSet.Any(f => f.Id == id);
    }

    public ICollection<FilmItemList> GetFavoriteFilms(string userId)
    {
        return DbContext.Users
            .Include(u => u.FavoriteFilms)
            .ThenInclude(f => f.Genres)
            .FirstOrDefault(u => u.Id == userId)?.FavoriteFilms.Select(f => new FilmItemList
            {
                Id = f.Id,
                Title = f.Title,
                PosterUrl = f.PosterUrl ?? "",
                AverageRating = f.AverageRating ?? 0,
                TotalView = f.TotalView,
                Genres = f.Genres.Select(g => new SelectOption
                {
                    Value = g.Id,
                    Label = g.Name
                })
            }).ToList() ?? new List<FilmItemList>();
    }

    public void AddFilmToFavoriteList(User user, Film film)
    {
        user.FavoriteFilms.Add(film);
    }

    public void RemoveFilmFromFavoriteList(User user, Film film)
    {
        user.FavoriteFilms.Remove(film);
    }

    public Task<FilmItemDetail?> GetFilmDetail(int id)
    {
        return DbSet
            .Include(f => f.Genres)
            .Include(f => f.Country)
            .Include(f => f.Episodes)
            .Where(f => f.Id == id)
            .Select(f => new FilmItemDetail
            {
                Title = f.Title,
                OtherTitle = f.OtherTitle,
                Description = f.Description,
                TrailerUrl = f.TrailerUrl,
                ThumbnailUrl = f.ThumbnailUrl,
                Duration = f.Duration,
                AverageRating = f.AverageRating,
                TotalEpisode = f.TotalEpisode,
                VideoUrl = f.VideoUrl,
                DurationPerEpisode = f.DurationPerEpisode,
                Type = f.Type,
                Actor = f.Actor,
                Director = f.Director,
                TotalView = f.TotalView,
                ReleaseYear = f.ReleaseYear,
                Country = f.CountryId != null
                    ? new SelectOption
                    {
                        Value = f.CountryId ?? 0,
                        Label = f.Country!.Name ?? ""
                    }
                    : null,
                Genres = f.Genres.Select(g => new SelectOption
                {
                    Value = g.Id,
                    Label = g.Name
                }),
                Episodes = f.Episodes.OrderBy(e => e.Number).Select(e => new EpisodeItem
                {
                    Id = e.Id,
                    Number = e.Number,
                    Title = e.Title,
                    Duration = e.Duration,
                    ThumbnailUrl = e.ThumbnailUrl,
                    VideoUrl = e.VideoUrl
                }).ToList()
            }).FirstOrDefaultAsync();
    }
}