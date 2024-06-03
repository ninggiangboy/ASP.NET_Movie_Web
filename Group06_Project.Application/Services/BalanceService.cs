using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Group06_Project.Application.Services;

public class BalanceService : IBalanceService
{
    // private const decimal PointToVnd = 1_000;
    private readonly IUnitOfWork _unitOfWork;

    public BalanceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionModel> CreateTransactionAsync(decimal amountPoint, TransactionType type, string userId)
    {
        var transactionReference = await  GenerateTransactionIdAsync();
        var info =  await GenerateMessageInfoAsync(type);
        await _unitOfWork.Transactions.AddAsync(new Transaction
        {
            TransactionReference =  transactionReference,
            Description =  info,
            Amount = amountPoint,
            UserId = userId,
            Type = type,
            Status = TransactionStatus.Pending
        });
        await _unitOfWork.CommitAsync();
        return new TransactionModel
        {
            TransactionReference = transactionReference,
            Info =  info,
            Amount = amountPoint,
        };
    }

    public async Task ConfirmTransactionAsync(string transactionReference)
    {
        var transaction =  await _unitOfWork.Transactions.GetByTransactionReference(transactionReference) 
            ?? throw new ArgumentException("Not found transaction");
        if (transaction.Status != TransactionStatus.Pending)
        {
            throw new ArgumentException("Transaction is not pending");
        }
        transaction.Status = TransactionStatus.Success;
        transaction.User.Balance += transaction.Amount;
        await _unitOfWork.Transactions.UpdateAsync(transaction);
        await _unitOfWork.CommitAsync();
    }

    private Task<string> GenerateTransactionIdAsync()
    {
        var dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        var guid = Guid.NewGuid().ToString();
        var hash = guid.GetHashCode().ToString("x"); // Convert to hexadecimal for shorter string
        var id = $"{dateTime}{hash}";
        return Task.FromResult(id);
    }
    
    private Task<string> GenerateMessageInfoAsync(TransactionType type)
    {
        return Task.FromResult(type switch
        {
            TransactionType.TopUp => "Top up balance",
            _ => "Unknown"
        });
    }
}