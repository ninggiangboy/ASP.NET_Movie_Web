using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFavoriteService
{
    ICollection<FilmItemList> GetFilmFavoriteList(string userId);
    void AddFilmToFavoriteList(User user, int filmId);
    void RemoveFilmFromFavoriteList(User user, int filmId);
    Task ToggleFavoriteFilm(int id, string userId);
    Task<bool> IsFavoriteFilm(FilmItemDetail existFilm, string userId);
}