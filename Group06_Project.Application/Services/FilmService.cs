using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class FilmService : IFilmService
{
    private const int HomeFilmListSize = 18;
    private readonly IUnitOfWork _unitOfWork;

    public FilmService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Page<FilmItemList> GetLatestFilm()
    {
        return GetHomeFilmList("CreatedAt DESC");
    }

    public Page<FilmItemList> GetPopularFilm()
    {
        return GetHomeFilmList("TotalView DESC, CreatedAt DESC");
    }

    public Page<FilmItemList> GetFeatureFilm()
    {
        return GetHomeFilmList("AverageRating DESC, CreatedAt DESC");
    }

    public Page<FilmItemList> GetFilmList(string? search, int? genre, int? country, FilmType? type, string? sort,
        int? pageNo)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = pageNo ?? 1,
            Size = HomeFilmListSize,
            Sort = sort
        };
        Expression<Func<Film, bool>> predicate = f =>
            (
                string.IsNullOrEmpty(search)
                || f.Title.Contains(search)
                || (!string.IsNullOrEmpty(f.OtherTitle) && f.OtherTitle.Contains(search))
                || (!string.IsNullOrEmpty(f.Actor) && f.Actor.Contains(search))
                || (!string.IsNullOrEmpty(f.Director) && f.Director.Contains(search))
            )
            && (!genre.HasValue || f.Genres.Any(g => g.Id == genre))
            && (!country.HasValue || f.CountryId == country)
            && (!type.HasValue || f.Type == type);
        return _unitOfWork.Films.GetFilmList(pageRequest, predicate);
    }

    public Task<FilmItemDetail?> GetFilmDetail(int id)
    {
        return _unitOfWork.Films.GetFilmDetail(id);
    }

    private Page<FilmItemList> GetHomeFilmList(string criteria)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = 1,
            Size = HomeFilmListSize,
            Sort = criteria
        };
        return _unitOfWork.Films.GetFilmList(pageRequest, null);
    }
}