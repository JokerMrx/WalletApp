using WalletApp.Core.Constants;

namespace WalletApp.BL.Utils;

public class CardBalanceRandom : ICardBalanceRandom
{
    public decimal RndBalance()
    {
        const decimal min = (decimal)Card.MinBalance;
        const decimal max = (decimal)Card.MaxBalance;
        var random = new Random();
        
        return decimal.Round((decimal) random.NextDouble() * (max - min) + min, 2);
    }
}