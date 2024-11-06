using Microsoft.EntityFrameworkCore;
using WalletApp.Core.Models;

namespace WalletApp.BL.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}