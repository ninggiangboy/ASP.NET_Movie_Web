using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IFilmRepository : IRepositoryBase<Film, int>
{
    Page<FilmItemList> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>>? predicate);
    bool ExistsById(int id);
    ICollection<FilmItemList> GetFavoriteFilms(string userId);
    void AddFilmToFavoriteList(User user, Film film);
    void RemoveFilmFromFavoriteList(User user, Film film);
    Task<FilmItemDetail?> GetFilmDetail(int id);
    void DeleteFilm(Film film);
    Film GetFilmByIdWithGenresAndCountry(int id);
    Task ToggleFavoriteFilm(string userId, int filmId);
    Task<bool> IsFavoriteFilm(int existFilmId, string userId);
    void AddView(int filmId, int viewCount);
    Task<IEnumerable<FilmListExport>> GetAllFilmList();
    Task ToggleVisibleFilm(int id);
}