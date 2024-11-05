using System.ComponentModel.DataAnnotations;

namespace WalletApp.Core.Models;

public class User : Base
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required] public string PasswordHash { get; set; }
}