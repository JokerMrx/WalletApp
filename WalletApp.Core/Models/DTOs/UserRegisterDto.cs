using System.ComponentModel.DataAnnotations;

namespace WalletApp.Core.Models.DTOs;

public class UserRegisterDto
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
}