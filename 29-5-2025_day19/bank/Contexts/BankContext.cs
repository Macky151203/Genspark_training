namespace Bank.Contexts;

using Bank.Models;
using Microsoft.EntityFrameworkCore;
public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>()
        .HasKey(a => a.AccountNumber);

        modelBuilder.Entity<BankAccount>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();

        modelBuilder.Entity<BankAccount>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.BankAccount)
            .HasForeignKey(t => t.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);
       
    }

}
