using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFavoriteService
{
    ICollection<FilmHomeModel> GetFilmFavoriteList(string userId);
    void AddFilmToFavoriteList();
    void RemoveFilmFromFavoriteList();
}