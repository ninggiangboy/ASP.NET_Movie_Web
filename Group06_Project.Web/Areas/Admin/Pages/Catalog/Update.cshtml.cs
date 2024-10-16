﻿using System.ComponentModel.DataAnnotations;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.Web.Areas.Admin.Pages.Catalog;

public class Update : PageModel
{
    private readonly ICountryService _countryService;
    private readonly IFilmService _filmService;
    private readonly IGenreService _genreService;

    public Update(IFilmService filmService, ICountryService countryService, IGenreService genreService)
    {
        _filmService = filmService;
        _countryService = countryService;
        _genreService = genreService;
    }

    [BindProperty(SupportsGet = true)] public int? Id { get; set; }
    [BindProperty] [Required] public string Title { get; set; } = default!;

    [BindProperty] public string? OtherTitle { get; set; }

    [BindProperty] [Required] public string? Description { get; set; }

    [BindProperty] public int? ReleaseYear { get; set; }

    [BindProperty] public int? Duration { get; set; }

    [BindProperty] public decimal? AverageRating { get; set; }

    [BindProperty] public int? TotalEpisode { get; set; }

    [BindProperty] public int? DurationPerEpisode { get; set; }

    [BindProperty] [Required] public string? Actor { get; set; }

    [BindProperty] [Required] public string? Director { get; set; }

    [BindProperty] [Required] public int CountryId { get; set; }
    public IEnumerable<SelectOption> FilmGenres { get; set; } = default!; // Initialize to avoid null reference

    [BindProperty] public int Type { get; set; }

    [BindProperty] public IFormFile? PosterFile { get; set; }
    [BindProperty] public IFormFile? VideoFile { get; set; }
    [BindProperty] public IFormFile? ThumbnailFile { get; set; }
    [BindProperty] public string? TrailerFile { get; set; }

    [BindProperty] public string? PosterUrl { get; set; }
    [BindProperty] public string? ThumbnailUrl { get; set; }
    [BindProperty] public string? VideoUrl { get; set; }
    [BindProperty] public string? TrailerUrl { get; set; }

    public IEnumerable<SelectOption> Countries { get; set; } = default!; // Initialize to avoid null reference
    public IEnumerable<SelectOption> Genres { get; set; } = default!; // Initialize to avoid null reference


    public Task<IActionResult> OnGetAsync()
    {
        if (Id == null) return Task.FromResult<IActionResult>(RedirectToRoute("./404.html"));
        LoadData();
        return Task.FromResult<IActionResult>(Page());
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        // if (id == null) return RedirectToPage("./404.html");

        if (!ModelState.IsValid)
        {
            Console.WriteLine("Invalid model state: " + string.Join(", ",
                ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            LoadData();
            return Page();
        }

        try
        {
            var updateFilm = new FilmItemUpdate
            {
                Id = id.Value,
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
                CountryId = CountryId,
                Type = Type switch
                {
                    0 => FilmType.Movie,
                    1 => FilmType.Series,
                    _ => FilmType.Movie
                },
                Genres = Request.Form["FilmGenres"].Select(g => new Genre { Id = int.Parse(g) }).ToList(),

                ThumbnailFile = ThumbnailFile,
                PosterFile = PosterFile,
                VideoFile = VideoFile,
                TrailerUrl = TrailerUrl
            };
            await _filmService.UpdateFilm(updateFilm);
            return RedirectToPage("./Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Page();
        }
    }

    private void LoadData()
    {
        Countries = _countryService.GetCountryHomeItems()
            .Select(c => new SelectOption { Value = c.Id, Label = c.Name });
        Genres = _genreService.GetGenreHomeItems().Select(g => new SelectOption { Value = g.Id, Label = g.Name });
        try
        {
            if (Id != null)
            {
                var film = _filmService.GetFilmDetailWithURL(Id.Value).Result;
                // if (film == null) RedirectToPage("./404.html");
                Title = film.Title;
                OtherTitle = film.OtherTitle;
                Description = film.Description;
                ReleaseYear = film.ReleaseYear;
                Duration = film.Duration;
                AverageRating = film.AverageRating;
                FilmGenres = film.Genres;
                Actor = film.Actor;
                Director = film.Director;
                CountryId = film.Country?.Value ?? 0;
                TotalEpisode = film.TotalEpisode;
                DurationPerEpisode = film.DurationPerEpisode;
                PosterUrl = film.PosterUrl;
                ThumbnailUrl = film.ThumbnailUrl;
                VideoUrl = film.VideoUrl;
                TrailerUrl = film.TrailerUrl;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}