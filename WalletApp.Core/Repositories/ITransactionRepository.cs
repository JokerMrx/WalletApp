using WalletApp.Core.Models;

namespace WalletApp.Core.Repositories;

public interface ITransactionRepository : IBaseRepository<Transaction, Transaction>
{
    public Task<IEnumerable<Transaction>> GetAllAsync(Guid cardId, PageParams pageParams);
}