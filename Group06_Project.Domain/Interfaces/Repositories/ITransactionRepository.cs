using Group06_Project.Domain.Entities;

namespace Group06_Project.Domain.Interfaces.Repositories;

public interface ITransactionRepository : IRepositoryBase<Transaction, string>
{
    Task<Transaction?> GetByTransactionReference(string transactionId);
}