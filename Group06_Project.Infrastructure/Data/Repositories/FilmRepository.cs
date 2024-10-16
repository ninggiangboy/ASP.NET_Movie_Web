using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class FilmRepository : RepositoryBase<Film, int>, IFilmRepository
{
    private readonly IConfigurationProvider _mapper;

    public FilmRepository(ApplicationDbContext appDbContext, IConfigurationProvider mapper) : base(appDbContext)
    {
        _mapper = mapper;
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
            .ProjectTo<FilmItemList>(_mapper);
        return new Page<FilmItemList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<FilmItemList>() : data.ToList(),
            Sort = pageRequest.Sort
        };
    }

    public bool ExistsById(int id)
    {
        return DbSet.Any(f => f.Id == id);
    }

    public ICollection<FilmItemList> GetFavoriteFilms(string userId)
    {
        return DbSet
            .Include(f => f.Genres)
            .Where(f => f.Followers.Any(u => u.Id == userId) && f.IsVisible)
            .ProjectTo<FilmItemList>(_mapper)
            .ToList();
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
            .Where(f => f.Id == id)
            .ProjectTo<FilmItemDetail>(_mapper)
            .FirstOrDefaultAsync();
    }

    public void DeleteFilm(Film film)
    {
        DbSet.Remove(film);
    }

    public Film? GetFilmByIdWithGenresAndCountry(int id)
    {
        return DbSet.Include(f => f.Genres)
            .Include(f => f.Country)
            .FirstOrDefault(f => f.Id == id);
    }

    public Task ToggleFavoriteFilm(string userId, int filmId)
    {
        var film = DbSet.Include(f => f.Followers).FirstOrDefault(f => f.Id == filmId);
        var user = DbContext.Users.FirstOrDefault(u => u.Id == userId)!;
        if (film!.Followers.Any(u => u.Id == userId))
            film.Followers.Remove(user);
        else
            film.Followers.Add(user);
        DbSet.Update(film);
        return Task.CompletedTask;
    }

    public Task<bool> IsFavoriteFilm(int existFilmId, string userId)
    {
        return DbSet.AnyAsync(f => f.Id == existFilmId && f.Followers.Any(u => u.Id == userId));
    }

    public void AddView(int filmId, int viewCount)
    {
        var film = DbSet.Find(filmId) ?? throw new Exception("Film not found");
        film.TotalView += viewCount;
        DbSet.Update(film);
    }

    public async Task<IEnumerable<FilmListExport>> GetAllFilmList()
    {
        return await DbSet
            .Include(f => f.Genres)
            .Include(f => f.Country)
            .ProjectTo<FilmListExport>(_mapper)
            .ToListAsync();
    }

    public Task ToggleVisibleFilm(int id)
    {
        var film = DbSet.Find(id) ?? throw new Exception("Film not found");
        film.IsVisible = !film.IsVisible;
        DbSet.Update(film);
        return Task.CompletedTask;
    }
}