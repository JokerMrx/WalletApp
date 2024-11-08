using Microsoft.EntityFrameworkCore;
using WalletApp.BL.Contexts;
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
        var transaction = _appDbContext.Transactions.Add(entity).Entity;
        await _appDbContext.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        var transaction = await _appDbContext.Transactions.SingleAsync(t => t.Id == id);

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(Guid cardId, PageParams pageParams)
    {
        var skip = (pageParams.Page - 1) * pageParams.Size;
        var transactions = await _appDbContext.Transactions
            .Where(e => e.CardId.Equals(cardId))
            .Skip(skip)
            .Take(pageParams.Size)
            .ToArrayAsync();

        return transactions;
    }
}