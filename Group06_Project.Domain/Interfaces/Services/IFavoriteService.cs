namespace Group06_Project.Domain.Interfaces.Services;

public interface IFavoriteService
{
    void GetFilmFavoriteList();
    void AddFilmToFavoriteList();
    void RemoveFilmFromFavoriteList();
}