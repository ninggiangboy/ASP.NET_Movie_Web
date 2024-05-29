namespace Group06_Project.Domain.Interfaces.Services;

public interface IFilmService
{
    void GetLatestFilm();
    void GetPopularFilm();
    void GetFeatureFilm();
    void GetFilmDetail();
}