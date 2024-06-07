using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Application.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<SelectListItem> GetGenresHomeList()
    {
        return _unitOfWork.Genres.GetAllGenresHomeModel();
    }

    public IEnumerable<HomeItem> GetGenreHomeItems()
    {
        return _unitOfWork.Genres.GetAllGenreHomeItems();
    }
}