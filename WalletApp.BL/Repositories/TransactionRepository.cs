using Microsoft.EntityFrameworkCore;
using WalletApp.BL.Contexts;
using WalletApp.Core.Enums;
using WalletApp.Core.Models;
using WalletApp.Core.Repositories;

namespace WalletApp.BL.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _appDbContext;

    public TransactionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Transaction> CreateAsync(Transaction entity)
    {
        var transaction = await _appDbContext.Transactions.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();

        return transaction.Entity;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        var transaction = await _appDbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(Guid cardId, PageParams pageParams)
    {
        var skip = (pageParams.Page - 1) * pageParams.Size;
        var transactions = await _appDbContext.Transactions
            .Where(e => e.CardId.Equals(cardId))
            .Skip(skip)
            .Take(pageParams.Size).OrderByDescending(en => en.CreatedAt)
            .ToArrayAsync();

        return transactions;
    }

    public async Task<Transaction> ApproveAsync(Guid transactionId, Guid userId)
    {
        var transaction = await GetByIdAsync(transactionId);

        if (transaction == null)
        {
            throw new KeyNotFoundException("Card not found");
        }

        transaction.AuthorizedUserId = userId;
        transaction.StatusType = TransactionStatusType.Approved;
        _appDbContext.Transactions.Update(transaction);
        await _appDbContext.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> RejectAsync(Guid transactionId, Guid userId)
    {
        var transaction = await GetByIdAsync(transactionId);

        if (transaction == null)
        {
            throw new KeyNotFoundException("Card not found");
        }

        transaction.AuthorizedUserId = userId;
        transaction.StatusType = TransactionStatusType.Rejected;
        _appDbContext.Transactions.Update(transaction);
        await _appDbContext.SaveChangesAsync();

        return transaction;
    }
}