using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Group06_Project.RazorPage.Areas.Admin.Pages.Comment
{
	public class CommentModel : PageModel
	{
		private readonly ICommentService _commentService;

		public Page<CommentList> Comments { get; set; }
		[BindProperty(SupportsGet = true)]
		public int PageNumber { get; set; } = 1;

		public CommentModel(ICommentService commentService)
		{
			_commentService = commentService;
			Comments = new Page<CommentList>();
		}

		public void OnGet()
		{
			Comments = _commentService.GetAllComments(PageNumber);

		}

		public IActionResult OnPostSearch(string searchTerm)
		{
			Comments = _commentService.SearchComments(searchTerm, PageNumber);
			return Page();
		}

		public IActionResult OnPostDelete(int commentId)
		{
			_commentService.RemoveComment(commentId);
			return RedirectToPage(new { pageNumber = 1 });
		}
	}
}
