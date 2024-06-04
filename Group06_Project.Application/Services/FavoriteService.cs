using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IUnitOfWork _unitOfWork;

    public FavoriteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ICollection<FilmItemList> GetFilmFavoriteList(string userId)
    {
        var data = _unitOfWork.Films.GetFavoriteFilms(userId);
        return data;
    }

    public void AddFilmToFavoriteList(string userId, int filmId)
    {
        // var user = _unitOfWork.Users.GetByIdWithFavoriteFilms(userId);
        // if (user == null) throw new Exception("User not found");
        // var film = _unitOfWork.Films.GetById(filmId);
        // if (film == null) throw new Exception("Film not found");
        // _unitOfWork.Films.AddFilmToFavoriteList(user, film);
        // _unitOfWork.Commit();
    }

    public void RemoveFilmFromFavoriteList(string userId, int filmId)
    {
        // var user = _unitOfWork.Users.GetByIdWithFavoriteFilms(userId);
        // if (user == null) throw new Exception("User not found");
        // var film = _unitOfWork.Films.GetById(filmId);
        // if (film == null) throw new Exception("Film not found");
        // _unitOfWork.Films.RemoveFilmFromFavoriteList(user, film);
        // _unitOfWork.Commit();
    }
}