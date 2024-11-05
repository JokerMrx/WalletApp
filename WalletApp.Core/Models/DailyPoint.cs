namespace WalletApp.Core.Models;

public class DailyPoint : Base
{
    public int Point { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
}