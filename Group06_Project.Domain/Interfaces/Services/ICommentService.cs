using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface ICommentService
{
    Task AddCommentToFilm(int filmId, string commentText, string userId);
    void RemoveComment(int commentId);
    Page<CommentItem> GetCommentsByFilmId(int filmId, int commentPageNo);
    Page<CommentList> GetAllComments(int? page, string? sort, string? userId, int? filmId);
}