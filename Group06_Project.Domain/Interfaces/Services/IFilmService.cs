using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFilmService
{
    Page<FilmHomeModel> GetLatestFilm(int page, int size);
    Page<FilmHomeModel> GetPopularFilm(int page, int size);
    Page<FilmHomeModel> GetFeatureFilm(int page, int size);
    void GetFilmDetail();
}