using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Group06_Project.RazorPage.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentService _commentService;

        public IndexModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty(SupportsGet = true)]
        public int FilmId { get; set; }

        public Page<CommentItem> Comments { get; set; }

        [BindProperty]
        public CommentItem NewComment { get; set; }

        public void OnGet()
        {
            var commentsPage = _commentService.GetCommentsByFilmId(FilmId, 1);
            Comments = commentsPage;
        }

        public IActionResult OnPostAdd()
        {

            _commentService.AddCommentToFilm(FilmId, NewComment.Content, NewComment.UserName);
            return RedirectToPage(new { filmId = FilmId });
        }

        public IActionResult OnPostDelete(int commentId)
        {
            _commentService.RemoveComment(commentId);
            return RedirectToPage(new { filmId = FilmId });
        }
    }
}