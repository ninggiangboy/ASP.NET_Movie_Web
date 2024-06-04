using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IGenreService
{
    IEnumerable<SelectOption> GetGenresHomeList();
}