using System.ComponentModel.DataAnnotations;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.RazorPage.Areas.Admin.Pages.Catalog;

public class Create : PageModel
{
    private readonly ICountryService _countryService;
    private readonly IFilmService _filmService;
    private readonly IGenreService _genreService;

    public Create(IFilmService filmService, ICountryService countryService, IGenreService genreService)
    {
        _filmService = filmService;
        _countryService = countryService;
        _genreService = genreService;
    }


    [BindProperty] [Required] public string Title { get; set; }

    [BindProperty] public string OtherTitle { get; set; }

    [BindProperty] [Required] public string Description { get; set; }

    [BindProperty] [Required] public int ReleaseYear { get; set; }

    [BindProperty] [Required] public int Duration { get; set; }

    [BindProperty] public decimal? AverageRating { get; set; }

    [BindProperty] public int? TotalEpisode { get; set; }

    [BindProperty] public int? DurationPerEpisode { get; set; }

    [BindProperty] [Required] public int Type { get; set; }

    [BindProperty] public string Actor { get; set; }

    [BindProperty] public string Director { get; set; }

    [BindProperty] [Required] public int TotalView { get; set; }

    [BindProperty] [Required] public int CountryId { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile? PosterFile { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile? VideoFile { get; set; }

    [BindProperty] public string? TrailerUrl { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile? ThumbnailFile { get; set; }

    public IEnumerable<SelectOption> Countries { get; set; } = default!; // Initialize to avoid null reference
    public IEnumerable<SelectOption> Genres { get; set; } = default!; // Initialize to avoid null reference


    public Task<IActionResult> OnGetAsync()
    {
        LoadData();
        return Task.FromResult<IActionResult>(Page());
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadData();
            return Page();
        }

        try
        {
            var filmGenres = Request.Form["FilmGenres"].Select(g => new Genre { Id = int.Parse(g) }).ToList();
            var createFilm = new FilmItemCreate
            {
                Title = Title,
                OtherTitle = OtherTitle,
                Description = Description,
                ReleaseYear = ReleaseYear,
                Duration = Duration,
                AverageRating = AverageRating,
                TotalEpisode = TotalEpisode,
                DurationPerEpisode = DurationPerEpisode,
                Actor = Actor,
                Director = Director,
                TotalView = TotalView,
                CountryId = CountryId,
                Genres = filmGenres,

                TrailerUrl = TrailerUrl,
                VideoFile = VideoFile,
                PosterFile = PosterFile,
                ThumbnailFile = ThumbnailFile,

                Type = Type switch
                {
                    0 => FilmType.Movie,
                    1 => FilmType.Series,
                    _ => FilmType.Movie
                }
            };
            await _filmService.AddFilm(createFilm);
            return RedirectToPage("./Catalog/Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Page();
        }
    }

    private void LoadData()
    {
        try
        {
            Countries = _countryService.GetCountryHomeItems()
                .Select(c => new SelectOption { Value = c.Id, Label = c.Name });
            Genres = _genreService.GetGenreHomeItems().Select(g => new SelectOption { Value = g.Id, Label = g.Name });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}