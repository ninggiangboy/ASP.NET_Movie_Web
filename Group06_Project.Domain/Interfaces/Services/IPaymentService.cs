using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IPaymentService
{
    Task<string> CreatePaymentUrl(TransactionModel transaction);
    Task CheckPayment(string queryString);
    Task<string> GetTransactionReference(string queryString);
}