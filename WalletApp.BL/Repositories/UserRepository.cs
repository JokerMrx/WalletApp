using Microsoft.EntityFrameworkCore;
using WalletApp.BL.Contexts;
using WalletApp.Core.Models;
using WalletApp.Core.Repositories;

namespace WalletApp.BL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateAsync(User entity)
    {
        Console.WriteLine($"{entity.Id} {entity.FirstName} {entity.LastName} {entity.Email} {entity.CreatedAt} {entity.IsActive}");
        var user = _dbContext.Users.Add(entity).Entity;
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.SingleAsync(u => u.Id.Equals(id));

        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _dbContext.Users.SingleAsync(u => u.Email == email);

        return user;
    }

    public async Task<User> DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        user.IsActive = false;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}