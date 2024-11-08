using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalletApp.Core.Enums;

namespace WalletApp.Core.Models;

public class Transaction : Base
{
    [Range(Constants.Card.MinBalance, Constants.Card.MaxBalance, ErrorMessage = "Maximum transaction amount 1500")]
    public decimal Sum { get; set; }

    public TransactionTypeEnum TransactionType { get; set; }

    [Required(ErrorMessage = "Transaction name is required")]
    public string Name { get; set; }

    public string Description { get; set; }
    public TransactionStatusType StatusType { get; set; } = TransactionStatusType.Pending;
    [Url] public string IconUrl { get; set; } = Constants.Transaction.DefaultIconUrl;
    [ForeignKey("User")] public Guid AuthorizedUserId { get; set; }
    public User AuthorizedUser { get; set; }

    [ForeignKey("Card")] public Guid CardId { get; set; }
    public Card Card { get; set; }
}