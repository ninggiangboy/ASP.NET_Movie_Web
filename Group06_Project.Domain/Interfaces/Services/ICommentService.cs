namespace Group06_Project.Domain.Interfaces.Services;

public interface ICommentService
{
    void AddCommentToFilm(int filmId, string commentText, string userId);
    void RemoveComment(int commentId);
}