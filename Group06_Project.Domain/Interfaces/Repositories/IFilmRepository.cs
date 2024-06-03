using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;
using System.Linq.Expressions;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IFilmRepository : IRepositoryBase<Film, int>
{
    Page<FilmHomeModel> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>> predicate);
}