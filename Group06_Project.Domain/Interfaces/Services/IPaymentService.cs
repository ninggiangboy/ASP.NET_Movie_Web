namespace Group06_Project.Domain.Interfaces.Services;

public interface IPaymentService
{
    Task<string> CreatePaymentUrl(string orderId, string info, decimal amount);
}