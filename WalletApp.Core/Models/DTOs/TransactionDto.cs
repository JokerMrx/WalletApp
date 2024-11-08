using WalletApp.Core.Enums;

namespace WalletApp.Core.Models.DTOs;

public class TransactionDto : Base
{
    public decimal Sum { get; set; }
    public TransactionTypeEnum TransactionType { get; set; }
    public TransactionStatusType StatusType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? IconUrl { get; set; }
    public Guid CardId { get; set; }
}