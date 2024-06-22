using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface ICommentRepository : IRepositoryBase<Comment, int>
{
    void RemoveById(int commentId);
    public Page<CommentList> GetAllComments(PageRequest<Comment> commentPageNo);
    Page<CommentItem> GetByFilmId(int filmId, PageRequest<Comment> pageRequest);
    void UpdateComment(int commentId, string newContent);
	Page<CommentList> SearchComments(string searchTerm, PageRequest<Comment> pageRequest);
}