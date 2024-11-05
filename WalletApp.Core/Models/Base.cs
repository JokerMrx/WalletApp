using System.ComponentModel.DataAnnotations;

namespace WalletApp.Core.Models;

public class Base
{
    [Key] public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}