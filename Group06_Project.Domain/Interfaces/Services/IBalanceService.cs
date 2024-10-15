using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IBalanceService
{
    Task<TransactionModel> CreateTransactionAsync(decimal amountPoint, TransactionType type, string userId);

    Task ConfirmTransactionAsync(string transactionReference);
    Task PurchaseAsync(decimal amountPoint, string userId);
}