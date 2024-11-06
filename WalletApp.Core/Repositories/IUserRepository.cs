using WalletApp.Core.Models;
using WalletApp.Core.Models.DTOs;

namespace WalletApp.Core.Repositories;

public interface IUserRepository : IBaseRepository<UserDto, User>
{
    public Task<User> GetUserByEmailAsync(string email);
    public Task<User> DeleteAsync(Guid id);
}