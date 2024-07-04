using Group06_Project.Domain.Entities;
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

    public void AddFilmToFavoriteList(User user, int filmId)
    {
        var film = _unitOfWork.Films.GetById(filmId);
        if (film == null) throw new Exception("Film not found");
        _unitOfWork.Films.AddFilmToFavoriteList(user, film);
        _unitOfWork.Commit();
    }

    public void RemoveFilmFromFavoriteList(User user, int filmId)
    {
        var film = _unitOfWork.Films.GetById(filmId);
        if (film == null) throw new Exception("Film not found");
        _unitOfWork.Films.RemoveFilmFromFavoriteList(user, film);
        _unitOfWork.Commit();
    }

    public async Task ToggleFavoriteFilm(int id, string userId)
    {
        await _unitOfWork.Films.ToggleFavoriteFilm(userId, id);
        await _unitOfWork.CommitAsync();
    }

    public Task<bool> IsFavoriteFilm(FilmItemDetail existFilm, string userId)
    {
        return _unitOfWork.Films.IsFavoriteFilm(existFilm.Id, userId);
    }
}