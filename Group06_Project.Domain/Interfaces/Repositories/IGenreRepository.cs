using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface IGenreRepository : IRepositoryBase<Genre, int>
{
    IEnumerable<GenreHomeModel> GetAllGenresHomeModel();
}