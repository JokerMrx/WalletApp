namespace WalletApp.Core.Models.DTOs;

public record class UserDto(Guid Id, string Email, string FirstName, string LastName, DateTime CreatedAt);