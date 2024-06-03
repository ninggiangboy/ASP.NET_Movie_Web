using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.RazorPage.Pages;

public class IndexModel : PageModel
{
    private readonly ICountryService _countryService;
    private readonly IGenreService _genreService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IGenreService genreService, ICountryService countryService)
    {
        _logger = logger;
        _genreService = genreService;
        _countryService = countryService;
    }

    public IEnumerable<GenreOptions> GenreOptions { get; set; } = Array.Empty<GenreOptions>();
    public IEnumerable<CountryOptions> CountryOptions { get; set; } = Array.Empty<CountryOptions>();
    public IEnumerable<string> YearOptions { get; set; } = Array.Empty<string>();

    public void OnGet()
    {
        GenreOptions = _genreService.GetGenresHomeList();
        CountryOptions = _countryService.GetCountryOptionsList();
        var currentYear = DateTime.Now.Year;
        YearOptions = Enumerable.Range(currentYear - 3, 3).Select(x => x.ToString());
    }
}