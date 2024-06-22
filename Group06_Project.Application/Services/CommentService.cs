using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;

namespace Group06_Project.Application.Services;

public class CommentService : ICommentService
{
    private const int CommentPageSize = 5;
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

    public Page<CommentItem> GetCommentsByFilmId(int filmId, int commentPageNo)
    {
        var pageRequest = new PageRequest<Comment>
        {
            PageNumber = commentPageNo,
            Size = CommentPageSize,
            Sort = "Time Desc"
        };
        return _unitOfWork.Comments.GetByFilmId(filmId, pageRequest);
    }

    public void UpdateComment(int commentId, string newContent)
    {
        _unitOfWork.Comments.UpdateComment(commentId, newContent);
        _unitOfWork.Commit();
    }

    public Page<CommentList> GetAllComments(int commentPageNo)
    {
        var pageRequest = new PageRequest<Comment>
        {
            PageNumber = commentPageNo,
            Size = CommentPageSize,
            Sort = "Time Desc"
        };
        return _unitOfWork.Comments.GetAllComments(pageRequest);
    }
	public Page<CommentList> SearchComments(string searchTerm, int commentPageNo)
	{
		var pageRequest = new PageRequest<Comment>
		{
			PageNumber = commentPageNo,
			Size = CommentPageSize,
			Sort = "Time Desc"
		};

		return _unitOfWork.Comments.SearchComments(searchTerm, pageRequest);
	}
}