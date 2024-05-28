using System.Diagnostics;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Group06_Project.Web.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IHomeService homeService)
    {
        _logger = logger;
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            Genres = _homeService.GetGenresHomeModelList()
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}