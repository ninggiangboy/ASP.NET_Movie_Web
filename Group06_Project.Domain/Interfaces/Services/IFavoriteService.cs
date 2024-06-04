using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFavoriteService
{
    ICollection<FilmItemList> GetFilmFavoriteList(string userId);
    void AddFilmToFavoriteList(string userId, int filmId);
    void RemoveFilmFromFavoriteList(string userId, int filmId);
}