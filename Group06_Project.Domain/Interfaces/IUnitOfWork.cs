namespace Group06_Project.Domain.Interfaces;

public interface IUnitOfWork
{
    bool Commit();
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}