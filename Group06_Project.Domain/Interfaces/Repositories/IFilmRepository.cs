using Group06_Project.Domain.Entities;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IFilmRepository : IRepositoryBase<Film, int>
{
    IEnumerable<Film> GetList(string userId);
}