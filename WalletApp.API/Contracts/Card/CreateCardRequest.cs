using System.ComponentModel.DataAnnotations;

namespace WalletApp.API.Contracts.Card;

public class CreateCardRequest
{
    [Required] public Guid UserId { get; set; }
}