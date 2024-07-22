using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class CommentService : ICommentService
{
    private const int CommentPageSize = 10;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddCommentToFilm(int filmId, string commentText, string userId)
    {
        if (!_unitOfWork.Films.ExistsById(filmId)) throw new Exception("Film not found");
        var comment = new Comment
        {
            FilmId = filmId,
            Content = commentText,
            UserId = userId
        };
        _unitOfWork.Comments.Add(comment);
        await _unitOfWork.CommitAsync();
    }

    public void RemoveComment(int commentId)
    {
        _unitOfWork.Comments.RemoveById(commentId);
        _unitOfWork.Commit();
    }

    public async Task<Page<CommentItem>> GetCommentsByFilmId(int filmId, int commentPageNo)
    {
        var pageRequest = new PageRequest<Comment>
        {
            PageNumber = commentPageNo,
            Size = CommentPageSize,
            Sort = "Time Desc"
        };
        return await _unitOfWork.Comments.GetByFilmId(filmId, pageRequest);
    }

    public Page<CommentList> GetAllComments(int? page, string? sort, string? userId, int? filmId)
    {
        var pageRequest = new PageRequest<Comment>
        {
            PageNumber = page ?? 1,
            Size = CommentPageSize,
            Sort = sort
        };
        Expression<Func<Comment, bool>> predicate = c =>
            (string.IsNullOrEmpty(userId) || c.UserId == userId)
            && (!filmId.HasValue || c.FilmId == filmId);
        return _unitOfWork.Comments.GetAllBy(pageRequest, predicate);
    }
}