using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class FilmService : IFilmService
{
    private readonly IUnitOfWork _unitOfWork;

    public FilmService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void GetFilmDetail()
    {
        throw new NotImplementedException();
    }

    public Page<FilmHomeModel> GetLatestFilm(int page, int size)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = page,
            Size = size,
            Sort = "ReleaseYear DESC, CreatedAt DESC",
        };
        return _unitOfWork.Films.GetFilmList(pageRequest, null);
    }

    public Page<FilmHomeModel> GetPopularFilm(int page, int size)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = page,
            Size = size,
            Sort = "TotalView DESC, CreatedAt DESC",
        };
        return _unitOfWork.Films.GetFilmList(pageRequest, null);
    }

    public Page<FilmHomeModel> GetFeatureFilm(int page, int size)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = page,
            Size = size,
            Sort = "AverageRating DESC, CreatedAt DESC",
        };
        return _unitOfWork.Films.GetFilmList(pageRequest, null);
    }
}