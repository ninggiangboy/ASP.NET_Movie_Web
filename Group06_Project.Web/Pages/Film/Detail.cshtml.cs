using System.Security.Claims;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Group06_Project.Infrastructure.RealTime;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;

namespace Group06_Project.Web.Pages.Film;

public class Detail : PageModel
{
    private readonly IBalanceService _balanceService;
    private readonly ICommentService _commentService;
    private readonly IFavoriteService _favoriteService;
    private readonly IFilmService _filmService;
    private readonly IHubContext<SignalRHub> _hub;
    private readonly UserManager<User> _userManager;

    public Detail(IFilmService filmService, ICommentService commentService, IHubContext<SignalRHub> hub,
        IFavoriteService favoriteService, UserManager<User> userManager, IBalanceService balanceService)
    {
        _filmService = filmService;
        _commentService = commentService;
        _hub = hub;
        _favoriteService = favoriteService;
        _userManager = userManager;
        _balanceService = balanceService;
    }

    public FilmItemDetail Film { get; set; } = null!;
    public Page<CommentItem> Comments { get; set; } = null!;

    [BindProperty] public InputCommentModel InputComment { get; set; } = new();
    public bool IsFavorite { get; set; }

    public async Task<IActionResult> OnGetCommentsAsync(int id, int pageNo)
    {
        var comments = await _commentService.GetCommentsByFilmId(id, pageNo);
        return new JsonResult(comments);
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var existFilm = await _filmService.GetFilmDetail(id.Value);
        if (existFilm == null) return NotFound();
        Film = existFilm;
        Comments = new Page<CommentItem>();
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IsFavorite = await _favoriteService.IsFavoriteFilm(existFilm, userId!);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddCommentAsync(int id)
    {
        if (!ModelState.IsValid) return RedirectToPage(new { id });
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _commentService.AddCommentToFilm(id, InputComment.Content, userId);
        await _hub.Clients.Group(id.ToString()).SendAsync("ReceiveComment");
        return Redirect(Url.Page("Detail", new { id }) + "#comment");
    }

    public async Task<IActionResult> OnPostFavoriteAsync(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        try
        {
            await _balanceService.PurchaseAsync(10, userId);
        }
        catch (Exception e)
        {
            TempData["Error"] = "Not enough balance";
            return Redirect(Url.Page("Detail", new { id })!);
        }
        await _favoriteService.ToggleFavoriteFilm(id, userId);
        return Redirect(Url.Page("Detail", new { id })!);
    }

    public class InputCommentModel
    {
        [Required] public string Content { get; set; } = null!;
    }
}