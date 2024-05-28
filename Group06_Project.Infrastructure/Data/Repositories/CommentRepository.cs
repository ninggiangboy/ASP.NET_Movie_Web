using System.Linq.Dynamic.Core;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class CommentRepository : RepositoryBase<Comment, int>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<Comment> GetByFilmId(int filmId)
    {
        return DbSet.Where(x => x.FilmId == filmId).ToList();
    }
}