using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.RazorPage.Pages;

public class IndexModel : PageModel
{
    private readonly ICountryService _countryService;
    private readonly IFilmService _filmService;
    private readonly IGenreService _genreService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IGenreService genreService, ICountryService countryService,
        IFilmService filmService)
    {
        _logger = logger;
        _genreService = genreService;
        _countryService = countryService;
        _filmService = filmService;
    }

    public IEnumerable<HomeItem> GenreItems { get; set; } = null!;
    public IEnumerable<HomeItem> CountryItems { get; set; } = null!;
    public Page<FilmItemList> LatestFilms { get; set; } = null!;
    public Page<FilmItemList> PopularFilms { get; set; } = null!;
    public Page<FilmItemList> FeatureFilms { get; set; } = null!;

    public void OnGet()
    {
        LatestFilms = _filmService.GetLatestFilm();
        PopularFilms = _filmService.GetPopularFilm();
        FeatureFilms = _filmService.GetFeatureFilm();

        CountryItems = _countryService.GetCountryHomeItems();
        GenreItems = _genreService.GetGenreHomeItems();
    }
}