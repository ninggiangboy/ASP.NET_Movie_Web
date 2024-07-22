using Group06_Project.Domain.Interfaces.Repositories;

namespace Group06_Project.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICommentRepository Comments { get; }
    ICountryRepository Countries { get; }
    IGenreRepository Genres { get; }
    IFilmRepository Films { get; }
    IRatingRepository Ratings { get; }
    ITransactionRepository Transactions { get; }
    bool Commit();
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}