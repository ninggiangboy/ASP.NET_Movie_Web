using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<SelectOption> GetGenresHomeList()
    {
        // return _unitOfWork.Genres.GetAll().Select(g => new GenreHomeModel()
        // {
        //     Id = g.Id,
        //     Name = g.Name
        // });
        return _unitOfWork.Genres.GetAllGenresHomeModel();
    }
}