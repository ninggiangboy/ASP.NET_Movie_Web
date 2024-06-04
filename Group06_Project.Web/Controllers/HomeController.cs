using System.Diagnostics;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Group06_Project.Web.Controllers;

public class HomeController : Controller
{
    private readonly IGenreService _genreService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IGenreService genreService)
    {
        _logger = logger;
        _genreService = genreService;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            Genres = _genreService.GetGenresHomeList() 
        };
        return View(viewModel);
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