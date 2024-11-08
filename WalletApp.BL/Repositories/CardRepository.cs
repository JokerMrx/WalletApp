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

    public async Task<Card?> GetByIdAsync(Guid id)
    {
        var card = await _appDbContext.Cards.FirstOrDefaultAsync(c => c.Id.Equals(id));

        if (card != null)
        {
            card.CurrentMonth = DateTime.Today.Month;
        }

        return card;
    }

    public async Task<Card> DepositFundsAsync(Guid id, decimal amount)
    {
        var card = await GetByIdAsync(id);

        if (card == null)
        {
            throw new KeyNotFoundException("Card not found");
        }

        var willBeFunds = card.Balance + amount;

        if (willBeFunds < Core.Constants.Card.MinBalance || willBeFunds > Core.Constants.Card.MaxBalance)
        {
            throw new InvalidOperationException(
                "As a result of this transaction, the balance sheet amount will be outside this range");
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
        var card = await GetByIdAsync(id);

        if (card == null)
        {
            throw new KeyNotFoundException("Card not found");
        }

        var willBeFunds = card.Balance - amount;

        if (willBeFunds < Core.Constants.Card.MinBalance)
        {
            throw new InvalidOperationException("Not enough funds");
        }

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