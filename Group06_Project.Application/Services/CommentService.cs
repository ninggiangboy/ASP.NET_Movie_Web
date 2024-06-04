using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;

namespace Group06_Project.Application.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddCommentToFilm(int filmId, string commentText, string userId)
    {
        if (!_unitOfWork.Films.ExistsById(filmId)) throw new Exception("Film not found");
        var comment = new Comment
        {
            FilmId = filmId,
            Content = commentText,
            UserId = userId
        };
        _unitOfWork.Comments.Add(comment);
        _unitOfWork.Commit();
    }

    public void RemoveComment(int commentId)
    {
        _unitOfWork.Comments.RemoveById(commentId);
        _unitOfWork.Commit();
    }
}