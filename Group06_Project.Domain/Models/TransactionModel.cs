using Group06_Project.Domain.Enums;

namespace Group06_Project.Domain.Models;

public class TransactionModel
{
    public string TransactionReference { get; init; } = null!;
    public string Info { get; init; } = string.Empty;
    public decimal Amount { get; init; }
}