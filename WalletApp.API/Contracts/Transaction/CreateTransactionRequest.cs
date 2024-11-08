using WalletApp.Core.Enums;

namespace WalletApp.API.Contracts.Transaction;

public class CreateTransactionRequest
{
    public decimal Sum { get; set; }
    public TransactionTypeEnum TransactionType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CardId { get; set; }
    public Guid AuthorizedUserId { get; set; }
}