using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Group06_Project.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IUnitOfWork _unitOfWork;

    public FavoriteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ICollection<FilmHomeModel> GetFilmFavoriteList(string userId)
    {
        var data = _unitOfWork.Films.GetFavoriteFilms(userId);
        return data;
    }

    public void AddFilmToFavoriteList()
    {
        throw new NotImplementedException();
    }

    public void RemoveFilmFromFavoriteList()
    {
        throw new NotImplementedException();
    }
}