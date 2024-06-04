using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFilmService
{
    Page<FilmItemList> GetLatestFilm();
    Page<FilmItemList> GetPopularFilm();
    Page<FilmItemList> GetFeatureFilm();
    FilmItemDetail GetFilmDetail(int id);
}