using WalletApp.Core.Models;

namespace WalletApp.Core.Repositories;

public interface ITransactionRepository : IBaseRepository<Transaction, Transaction>
{
    public Task<IEnumerable<Transaction>> GetAllAsync(Guid cardId, PageParams pageParams);
    public Task<Transaction> ApproveAsync(Guid transactionId, Guid userId);
    public Task<Transaction> RejectAsync(Guid transactionId, Guid userId);
}