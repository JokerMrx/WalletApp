using Microsoft.EntityFrameworkCore;
using WalletApp.BL.Contexts;
using WalletApp.Core.Models;
using WalletApp.Core.Repositories;

namespace WalletApp.BL.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _appDbContext;

    public CardRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Card> CreateAsync(Card entity)
    {
        var card = await _appDbContext.Cards.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();

        return card.Entity;
    }

    public async Task<Card> GetByIdAsync(Guid id)
    {
        var card = await _appDbContext.Cards.SingleAsync(c => c.Id.Equals(id));
        card.CurrentMonth = DateTime.Today.Month;

        return card;
    }

    public async Task<Card> DepositFundsAsync(Guid id, decimal amount)
    {
        if (amount < Core.Constants.Card.MinBalance || amount > Core.Constants.Card.MaxBalance)
        {
            throw new ArgumentOutOfRangeException(nameof(amount)); // TODO message
        }

        var card = await GetByIdAsync(id);
        var willBeFunds = card.Balance + amount;

        if (willBeFunds > Core.Constants.Card.MaxBalance)
        {
            throw new InvalidOperationException(""); // TODO message
        }

        card.Balance = willBeFunds;
        card.Available -= amount;
        card.CurrentMonth = DateTime.Today.Month;
        _appDbContext.Cards.Update(card);
        await _appDbContext.SaveChangesAsync();

        return card;
    }

    public async Task<Card> WithdrawFundsAsync(Guid id, decimal amount)
    {
        if (amount < Core.Constants.Card.MinBalance || amount > Core.Constants.Card.MaxBalance)
        {
            throw new ArgumentOutOfRangeException(nameof(amount)); // TODO message
        }

        var card = await GetByIdAsync(id);

        if (card.Available < amount)
        {
            throw new InvalidOperationException("Not enough funds");
        }

        card.Balance -= amount;
        card.Available += amount;
        _appDbContext.Cards.Update(card);
        await _appDbContext.SaveChangesAsync();

        return card;
    }
}