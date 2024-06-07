using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Application.Services;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<SelectListItem> GetCountryOptionsList()
    {
        return _unitOfWork.Countries.GetAllCountryOptions();
    }

    public IEnumerable<HomeItem> GetCountryHomeItems()
    {
        return _unitOfWork.Countries.GetAllCountryHomeItems();
    }
}