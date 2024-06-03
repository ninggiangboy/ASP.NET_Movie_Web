using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class TransactionRepository : RepositoryBase<Transaction, string>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public Task<Transaction?> GetByTransactionReference(string transactionId)
    {
        return DbSet.Include(t => t.User).FirstOrDefaultAsync(t => t.TransactionReference == transactionId);
    }
}