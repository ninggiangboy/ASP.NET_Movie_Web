using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IFilmService
{
    Page<FilmItemList> GetLatestFilm();
    Page<FilmItemList> GetPopularFilm();
    Page<FilmItemList> GetFeatureFilm();
    Page<FilmItemList> GetFilmList(string? search, int? genre, int? country, FilmType? type, string? sort, int? pageNo);

    public Page<FilmItemList> GetFilmListDashboard(string? search, int? genre, int? country, FilmType? type,
        string? sort, int? pageNo);

    Task<FilmItemDetail?> GetFilmDetail(int id);
    Task<FilmItemCreate> AddFilm(FilmItemCreate film);
    Task<FilmItemUpdate> UpdateFilm(FilmItemUpdate film);
    Task<bool> DeleteFilm(int id);
    Task<FilmItemDetail?> GetFilmDetailWithURL(int id);
    void AddView(int filmId, int viewCount);
    Task<IEnumerable<FilmListExport>> GetAllFilmList();
    Task ToggleVisibleFilm(int id);
}