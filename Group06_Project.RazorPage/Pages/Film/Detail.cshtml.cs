using System.Security.Claims;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace Group06_Project.RazorPage.Pages.Film;

public class Detail : PageModel
{
    private readonly ICommentService _commentService;
    private readonly IFilmService _filmService;

    public Detail(IFilmService filmService, ICommentService commentService)
    {
        _filmService = filmService;
        _commentService = commentService;
    }

    [BindProperty(SupportsGet = true)] public int CommentPageNo { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public int? Episode { get; set; }
    public FilmItemDetail Film { get; set; } = null!;
    public Page<CommentItem> Comments { get; set; } = null!;

    [BindProperty] public InputCommentModel InputComment { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var existFilm = await _filmService.GetFilmDetail(id.Value);
        if (existFilm == null) return NotFound();
        Film = existFilm;

        if (Episode.HasValue)
        {
            var episode = Film.Episodes.FirstOrDefault(x => x.Id == Episode);

            if (episode != null) return NotFound();

            Film.VideoUrl = episode?.VideoUrl;
            Film.Title = episode?.Title ?? Film.Title;
        }
        else if (Film is { Type: FilmType.Series, VideoUrl: null })
        {
            Film.VideoUrl = Film.Episodes.FirstOrDefault()?.VideoUrl;
        }

        Comments = _commentService.GetCommentsByFilmId(existFilm.Id, CommentPageNo);
        return Page();
    }

    public async Task<IActionResult> OnPostAddCommentAsync(int id)
    {
        if (!ModelState.IsValid) return RedirectToPage(new { id });
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _commentService.AddCommentToFilm(id, InputComment.Content, userId);
        return Redirect(Url.Page("Detail", new { id }) + "#comment");
    }

    public class InputCommentModel
    {
        [Required] public string Content { get; set; } = null!;
    }
}