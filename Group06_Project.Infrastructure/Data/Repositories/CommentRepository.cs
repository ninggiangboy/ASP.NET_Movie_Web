using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class CommentRepository : RepositoryBase<Comment, int>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public void RemoveById(int commentId)
    {
        var comment = new Comment { Id = commentId };
        DbSet.Remove(comment);
    }
}