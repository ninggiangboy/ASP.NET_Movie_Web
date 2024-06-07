using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.RazorPage.Pages.Film;

public class FilmIndexModel : PageModel
{
    private readonly ICountryService _countryService;
    private readonly IFilmService _filmService;
    private readonly IGenreService _genreService;
    private readonly ILogger<FilmIndexModel> _logger;

    public FilmIndexModel(ICountryService countryService, IFilmService filmService, IGenreService genreService,
        ILogger<FilmIndexModel> logger)
    {
        _countryService = countryService;
        _filmService = filmService;
        _genreService = genreService;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)] public string? Search { get; set; }
    [BindProperty(SupportsGet = true)] public int? Genre { get; set; }
    [BindProperty(SupportsGet = true)] public int? Country { get; set; }
    [BindProperty(SupportsGet = true)] public string? Sort { get; set; } = "CreatedAt DESC";
    [BindProperty(SupportsGet = true)] public int? PageNo { get; set; }
    [BindProperty(SupportsGet = true)] public FilmType? Type { get; set; }
    public IEnumerable<SelectListItem> GenreOptions { get; set; } = null!;
    public IEnumerable<SelectListItem> CountryOptions { get; set; } = null!;

    public IEnumerable<SelectListItem> SortOptions { get; set; } = new List<SelectListItem>
    {
        new("Latest Update", "CreatedAt DESC"),
        new("New release", "ReleaseYear DESC, CreatedAt DESC"),
        new("Most Popular", "TotalView DESC, CreatedAt DESC"),
        new("Highest Rated", "AverageRating DESC, CreatedAt DESC")
    };

    public IEnumerable<SelectListItem> TypeOptions { get; set; } = new List<SelectListItem>
    {
        new("Movie and TV Series", ""),
        new("Movie", FilmType.Movie.ToString()),
        new("TV Series", FilmType.Series.ToString())
    };

    public Page<FilmItemList> Films { get; set; } = null!;

    public void OnGet()
    {
        GenreOptions = _genreService.GetGenresHomeList();
        CountryOptions = _countryService.GetCountryOptionsList();
        Films = _filmService.GetFilmList(Search, Genre, Country, Type, Sort, PageNo);
        _logger.LogInformation(Films.ToString());
    }
}