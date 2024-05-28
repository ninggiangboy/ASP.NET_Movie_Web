using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services.Home;

public class HomeService : IHomeService
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<GenreHomeModel> GetGenresHomeModelList()
    {
        // return _unitOfWork.Genres.GetAll().Select(g => new GenreHomeModel()
        // {
        //     Id = g.Id,
        //     Name = g.Name
        // });
        return _unitOfWork.Genres.GetAllGenresHomeModel();
    }
}