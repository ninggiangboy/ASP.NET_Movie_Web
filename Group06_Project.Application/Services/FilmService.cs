using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class FilmService : IFilmService
{
    private const int HomeFilmListSize = 20;
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

    public FilmItemDetail GetFilmDetail(int id)
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