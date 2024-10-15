using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;

namespace Group06_Project.Web.Areas.Admin.Pages.Catalog;

public class Index : PageModel
{
    private readonly ICountryService _countryService;
    private readonly IFilmService _filmService;
    private readonly IGenreService _genreService;
    private readonly ILogger<Index> _logger;

    public Index(ICountryService countryService, IFilmService filmService, IGenreService genreService,
        ILogger<Index> logger)
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

    public Page<FilmItemList> FilmItems { get; set; }

    public void OnGet(string search, int? genre, int? country, FilmType? type, string sort, int? pageNo)
    {
        GenreOptions = _genreService.GetGenresHomeList();
        CountryOptions = _countryService.GetCountryOptionsList();
        FilmItems = _filmService.GetFilmListDashboard(Search, Genre, Country, Type, Sort, PageNo);
    }

    public async Task<IActionResult> OnGetExportExcelsAsync()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var sheet = package.Workbook.Worksheets.Add("Movies");
            sheet.Cells.LoadFromCollection(await _filmService.GetAllFilmList(), true);
            await package.SaveAsync();
        }

        stream.Position = 0;
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Catalogs.xlsx");
    }

    public IActionResult OnGetSort(string sort)
    {
        Sort = sort;
        return RedirectToPage(new { Search, Genre, Country, Type, Sort, PageNo });
    }

    public IActionResult OnPostAsync()
    {
        return RedirectToPage(new { Search, Genre, Country, Type, Sort, PageNo = 1 });
    }

    public async Task<IActionResult> OnPostToggleVisibleAsync(int id, string? search, string? sort, int? pageNo)
    {
        await _filmService.ToggleVisibleFilm(id);
        return RedirectToPage(new { Search = search, Sort = sort, PageNo = pageNo });
    }
}