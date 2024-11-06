using WalletApp.Core.Models;

namespace WalletApp.Core.Repositories;

public interface ICardRepository : IBaseRepository<Card, Card>
{
    public Task<Card> DepositFundsAsync (Guid id, decimal amount);
    public Task<Card> WithdrawFundsAsync (Guid id, decimal amount);
}