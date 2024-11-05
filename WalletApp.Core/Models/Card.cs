using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletApp.Core.Models;

public class Card : Base
{
    [Range(Constants.Card.MinBalance, Constants.Card.MaxBalance, ErrorMessage = "The maximum available is 1500")]
    public decimal Available { get; set; }

    [Range(Constants.Card.MinBalance, Constants.Card.MaxBalance, ErrorMessage = "The maximum balance is 1500")]
    public decimal Balance { get; set; }
    public int CurrentMonth { get; set; } = DateTime.Today.Month;

    [ForeignKey("User")] public Guid UserId { get; set; }
    public User User { get; set; }
}