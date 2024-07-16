using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using NuGet.Packaging;

namespace Group06_Project.Application.Services;

public class FilmService : IFilmService
{
    private const int HomeFilmListSize = 18;
    private readonly IDistributedCache _cache;
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;

    public FilmService(IUnitOfWork unitOfWork, IStorageService storageService, IDistributedCache cache)
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
        _cache = cache;
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
        var films = _unitOfWork.Films.GetFilmList(pageRequest, predicate);
        foreach (var film in films.Data)
        {
             film.PosterUrl = _storageService.GetImageUrl(film.PosterUrl).Result;
        }
        return films;
    }

    public async Task<FilmItemDetail?> GetFilmDetail(int id)
    {
        var key = $"view_film_{id}";
        var view = await _cache.GetStringAsync(key) ?? "0";
        var viewCount = int.Parse(view);
        await _cache.SetStringAsync(key, (viewCount + 1).ToString());
        var film =  await _unitOfWork.Films.GetFilmDetail(id);
        if (film != null)
        {
            film.VideoUrl = film.VideoUrl != null ? await _storageService.GetVideoUrl(film.VideoUrl) : "";
            film.PosterUrl = film.PosterUrl != null ? await _storageService.GetImageUrl(film.PosterUrl) : "";
            film.TrailerUrl = film.TrailerUrl != null ? await _storageService.GetImageUrl(film.TrailerUrl) : "";
            film.ThumbnailUrl = film.ThumbnailUrl != null ? await _storageService.GetImageUrl(film.ThumbnailUrl) : "";
        }
        return film;
    }

    public async Task<FilmItemCreate> AddFilm(FilmItemCreate film)
    {
        var videoUrl = string.Empty;
        var thumbnailUrl = string.Empty;
        var posterUrl = string.Empty;
        if (film.VideoFile != null) videoUrl = await _storageService.UploadVideo(film.VideoFile);

        // if (film.TrailerFile != null) trailerUrl = await _storageService.UploadVideo(film.TrailerFile);

        if (film.ThumbnailFile != null) thumbnailUrl = await _storageService.UploadImage(film.ThumbnailFile);

        if (film.PosterFile != null) posterUrl = await _storageService.UploadImage(film.PosterFile);

        var filmGenre = _unitOfWork.Genres.GetGenreByIds(film.Genres.Select(g => g.Id)).ToList();
        var entity = new Film
        {
            Title = film.Title,
            OtherTitle = film.OtherTitle,
            Description = film.Description,
            VideoUrl = videoUrl,
            TrailerUrl = film.TrailerUrl,
            ThumbnailUrl = thumbnailUrl,
            PosterUrl = posterUrl,
            Duration = film.Duration,
            AverageRating = film.AverageRating,
            TotalEpisode = film.TotalEpisode,
            DurationPerEpisode = film.DurationPerEpisode,
            Type = film.Type,
            Actor = film.Actor,
            Director = film.Director,
            TotalView = film.TotalView ?? 0,
            ReleaseYear = film.ReleaseYear,
            CountryId = film.CountryId,
            CreatedAt = DateTime.Now,
            Genres = filmGenre
        };
        var filmCreated = _unitOfWork.Films.Add(entity);
        await _unitOfWork.CommitAsync();
        return film;
    }

    public async Task<FilmItemUpdate> UpdateFilm(FilmItemUpdate film)
    {
        try
        {
            var videoUrl = string.Empty;
            var thumbnailUrl = string.Empty;
            var posterUrl = string.Empty;
            var currentFilm = _unitOfWork.Films.GetFilmByIdWithGenresAndCountry(film.Id);

            if (film.VideoFile != null)
            {
                if (currentFilm.VideoUrl != null && currentFilm.VideoUrl != string.Empty)
                    _storageService.DeleteFile(currentFilm.VideoUrl);
                videoUrl = await _storageService.UploadVideo(film.VideoFile);
            }
            else
            {
                videoUrl = currentFilm.VideoUrl;
            }

            // if (film.TrailerFile != null )
            // {
            //     if (currentFilm.TrailerUrl != null && currentFilm.TrailerUrl != string.Empty)
            //     {
            //         _storageService.DeleteFile(currentFilm.TrailerUrl);
            //     }
            //     trailerUrl = await _storageService.UploadVideo(film.TrailerFile);
            // }
            // else
            // {
            // trailerUrl = currentFilm.TrailerUrl;
            // }

            if (film.ThumbnailFile != null)
            {
                if (currentFilm.ThumbnailUrl != null && currentFilm.ThumbnailUrl != string.Empty)
                    _storageService.DeleteFile(currentFilm.ThumbnailUrl);
                thumbnailUrl = await _storageService.UploadImage(film.ThumbnailFile);
            }
            else
            {
                thumbnailUrl = currentFilm.ThumbnailUrl;
            }

            if (film.PosterFile != null)
            {
                if (currentFilm.PosterUrl != null && currentFilm.PosterUrl != string.Empty)
                    _storageService.DeleteFile(currentFilm.PosterUrl);
                posterUrl = await _storageService.UploadImage(film.PosterFile);
            }
            else
            {
                posterUrl = currentFilm.PosterUrl;
            }

            // Get genre by id
            var filmGenre = _unitOfWork.Genres.GetGenreByIds(film.Genres.Select(g => g.Id)).ToList();
            // Update film
            currentFilm.Genres.Clear();
            currentFilm.Title = film.Title;
            currentFilm.OtherTitle = film.OtherTitle;
            currentFilm.Description = film.Description;
            currentFilm.VideoUrl = videoUrl;
            currentFilm.TrailerUrl = film.TrailerUrl;
            currentFilm.ThumbnailUrl = thumbnailUrl;
            currentFilm.PosterUrl = posterUrl;
            currentFilm.Duration = film.Duration;
            currentFilm.AverageRating = film.AverageRating;
            currentFilm.TotalEpisode = film.TotalEpisode;
            currentFilm.DurationPerEpisode = film.DurationPerEpisode;
            currentFilm.Type = film.Type;
            currentFilm.Actor = film.Actor;
            currentFilm.Director = film.Director;
            currentFilm.TotalView = film.TotalView ?? 0;
            currentFilm.ReleaseYear = film.ReleaseYear;
            currentFilm.CountryId = film.CountryId;
            currentFilm.Genres.AddRange(filmGenre);
            // Update film to database
            _unitOfWork.Films.Update(currentFilm);
            await _unitOfWork.CommitAsync();
            return film;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> DeleteFilm(int id)
    {
        try
        {
            var film = _unitOfWork.Films.GetById(id);
            if (film.PosterUrl != null && film.PosterUrl != string.Empty) _storageService.DeleteFile(film.PosterUrl);

            if (film.ThumbnailUrl != null && film.ThumbnailUrl != string.Empty)
                _storageService.DeleteFile(film.ThumbnailUrl);

            if (film.VideoUrl != null && film.VideoUrl != string.Empty) _storageService.DeleteFile(film.VideoUrl);

            if (film.TrailerUrl != null && film.TrailerUrl != string.Empty) _storageService.DeleteFile(film.TrailerUrl);

            _unitOfWork.Films.DeleteFilm(film);
            return await _unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return await Task.FromResult(false);
        }
    }

    public async Task<FilmItemDetail?> GetFilmDetailWithURL(int id)
    {
        try
        {
            var film = _unitOfWork.Films.GetFilmDetail(id).Result;
            if (film == null) return null;

            if (film.PosterUrl != null) film.PosterUrl = await _storageService.GetImageUrl(film.PosterUrl);

            if (film.ThumbnailUrl != null) film.ThumbnailUrl = await _storageService.GetImageUrl(film.ThumbnailUrl);

            if (film.VideoUrl != null) film.VideoUrl = await _storageService.GetVideoUrl(film.VideoUrl);

            if (film.TrailerUrl != null) film.TrailerUrl = await _storageService.GetVideoUrl(film.TrailerUrl);

            var filmDetail = new FilmItemDetail
            {
                Id = film.Id,
                Title = film.Title,
                OtherTitle = film.OtherTitle,
                Description = film.Description,
                VideoUrl = film.VideoUrl,
                TrailerUrl = film.TrailerUrl,
                ThumbnailUrl = film.ThumbnailUrl,
                PosterUrl = film.PosterUrl,
                Duration = film.Duration,
                AverageRating = film.AverageRating,
                TotalEpisode = film.TotalEpisode,
                DurationPerEpisode = film.DurationPerEpisode,
                Type = film.Type,
                Actor = film.Actor,
                Director = film.Director,
                TotalView = film.TotalView,
                ReleaseYear = film.ReleaseYear,
                Country = film.Country,
                Genres = film.Genres
            };
            return filmDetail;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private Page<FilmItemList> GetHomeFilmList(string criteria)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = 1,
            Size = HomeFilmListSize,
            Sort = criteria
        };
        var films = _unitOfWork.Films.GetFilmList(pageRequest, null);
        foreach (var film in films.Data)
        {
             film.PosterUrl = _storageService.GetImageUrl(film.PosterUrl).Result;
        }
        return films;
    }
}