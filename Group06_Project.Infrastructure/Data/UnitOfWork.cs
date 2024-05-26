using Group06_Project.Domain.Interfaces;

namespace Group06_Project.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public bool Commit()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}