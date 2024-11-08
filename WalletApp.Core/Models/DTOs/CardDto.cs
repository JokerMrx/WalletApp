namespace WalletApp.Core.Models.DTOs;

public class CardDto : Base
{
    public decimal Available { get; set; }
    
    public decimal Balance { get; set; }

    public int CurrentMonth { get; set; } = DateTime.Today.Month;

    public Guid UserId { get; set; }
}