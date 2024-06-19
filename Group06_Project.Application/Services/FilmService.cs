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
    private readonly IStorageService _storageService;

    public FilmService(IUnitOfWork unitOfWork, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
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

    public async Task<FilmItemCreate> AddFilm(FilmItemCreate film)
    {
        var videoUrl = string.Empty;
        var trailerUrl = string.Empty;
        var thumbnailUrl = string.Empty;
        var posterUrl = string.Empty;
        if (film.VideoFile != null)
        {
           videoUrl = await _storageService.UploadVideo(film.VideoFile);
        }
        if (film.TrailerFile != null)
        {
            trailerUrl = await _storageService.UploadVideo(film.TrailerFile);
        }
        if (film.ThumbnailFile != null)
        {
            thumbnailUrl = await _storageService.UploadImage(film.ThumbnailFile);
        }
        if (film.PosterFile != null)
        {
            posterUrl = await _storageService.UploadImage(film.PosterFile);
        }
        var filmGenre = _unitOfWork.Genres.GetGenreByIds(film.Genres.Select(g => g.Id)).ToList();
        var entity = new Film
        {
            Title = film.Title,
            OtherTitle = film.OtherTitle,
            Description = film.Description,
            VideoUrl = videoUrl,
            TrailerUrl = trailerUrl,
            ThumbnailUrl = thumbnailUrl,
            PosterUrl = posterUrl,
            Duration = film.Duration,
            AverageRating = film.AverageRating,
            TotalEpisode = film.TotalEpisode,
            DurationPerEpisode = film.DurationPerEpisode,
            Type = film.Type,
            Actor = film.Actor,
            Director = film.Director,
            TotalView = (film.TotalView??0),
            ReleaseYear = film.ReleaseYear,
            CountryId = film.CountryId,
            CreatedAt = DateTime.Now,
            Genres = filmGenre
        };
        var filmCreated = _unitOfWork.Films.Add(entity);
        await _unitOfWork.CommitAsync();
        return film;
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