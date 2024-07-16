using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.RazorPage.Areas.Admin.Pages.Comment;

public class CommentModel : PageModel
{
    private readonly ICommentService _commentService;


    public CommentModel(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public Page<CommentList> Comments { get; set; } = null!;

    [BindProperty(SupportsGet = true)] public int? PageNo { get; set; }

    [BindProperty(SupportsGet = true)] public string? Sort { get; set; }
    [BindProperty(SupportsGet = true)] public string? UserId { get; set; }
    [BindProperty(SupportsGet = true)] public int? FilmId { get; set; }

    public IEnumerable<SelectListItem> SortOptions { get; set; } = new List<SelectListItem>
    {
        new("Latest", "Time DESC"),
        new("Oldest", "Time ASC")
    };

    public void OnGet()
    {
        Comments = _commentService.GetAllComments(PageNo, Sort, UserId, FilmId);
    }

    public IActionResult OnPostDelete(int commentId, int? pageNumber)
    {
        _commentService.RemoveComment(commentId);
        return RedirectToPage(new { PageNo = pageNumber });
    }
}