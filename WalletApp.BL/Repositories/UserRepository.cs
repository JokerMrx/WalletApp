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
        var user = await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return user.Entity;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<User> DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        
        user.IsActive = false;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}