using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IFilmRepository : IRepositoryBase<Film, int>
{
    Page<FilmItemList> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>>? predicate);
    bool ExistsById(int id);
}