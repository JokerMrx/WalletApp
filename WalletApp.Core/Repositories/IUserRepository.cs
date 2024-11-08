using WalletApp.Core.Models;

namespace WalletApp.Core.Repositories;

public interface IUserRepository : IBaseRepository<User, User>
{
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<User> DeleteAsync(Guid id);
}