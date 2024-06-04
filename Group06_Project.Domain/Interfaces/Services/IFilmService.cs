using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFilmService
{
    Page<FilmItemList> GetLatestFilm(int page, int size);
    Page<FilmItemList> GetPopularFilm(int page, int size);
    Page<FilmItemList> GetFeatureFilm(int page, int size);
    void GetFilmDetail();
}