using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface ICommentService
{
    void AddCommentToFilm(int filmId, string commentText, string userId);
    void RemoveComment(int commentId);
    Page<CommentList> GetAllComments(int commentPageNo);
	Page<CommentList> GetAllCommentsByAsc(int commentPageNo);

	Page<CommentItem> GetCommentsByFilmId(int filmId, int commentPageNo);
    void UpdateComment(int commentId, string newContent);
    Page<CommentList> SearchComments(string searchTerm, int commentPageNo);
	Page<CommentList> SearchCommentsByAsc(string searchTerm, int commentPageNo);
}