using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.Web.Areas.Admin.Pages.Catalog;

public class Delete : PageModel
{
    private readonly IFilmService _filmService;

    public Delete(IFilmService filmService)
    {
        _filmService = filmService;
    }

    [BindProperty] public FilmItemDetail Film { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var film = await _filmService.GetFilmDetail(id.Value);
        if (film == null) return NotFound();
        Film = film;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();
        var result = await _filmService.DeleteFilm(id.Value);
        if (!result) return Page();
        return RedirectToPage("./Index");
    }
}