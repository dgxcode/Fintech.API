using Fintech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Infrastructure.Data
{
    public class FintechDbContext : DbContext
    {
        public FintechDbContext(DbContextOptions<FintechDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de relacionamento e constraints
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Transactions)
                .WithOne()
                .HasForeignKey(t => t.FromWalletId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
