using System.Linq.Expressions;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface ICommentRepository : IRepositoryBase<Comment, int>
{
    void RemoveById(int commentId);
    public Page<CommentList> GetAllBy(PageRequest<Comment> commentPageNo, Expression<Func<Comment, bool>>? predicate);
    Page<CommentItem> GetByFilmId(int filmId, PageRequest<Comment> pageRequest);
}