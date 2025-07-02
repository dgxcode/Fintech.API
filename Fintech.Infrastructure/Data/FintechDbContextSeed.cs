using Fintech.Domain.Entities;

namespace Fintech.Infrastructure.Data
{
    public static class FintechDbContextSeed
    {
        public static async Task SeedAsync(FintechDbContext context)
        {
            if (!context.Users.Any())
            {
                var user1 = new User(
                    name: "João Silva",
                    email: "joao@email.com",
                    passwordHash: BCrypt.Net.BCrypt.HashPassword("123456")
                );

                var user2 = new User(
                    name: "Maria Souza",
                    email: "maria@email.com",
                    passwordHash: BCrypt.Net.BCrypt.HashPassword("123456")
                );

                user1.Wallet.AddBalance(1000m);
                user2.Wallet.AddBalance(500m);

                context.Users.AddRange(user1, user2);

                await context.SaveChangesAsync();
            }
        }
    }
}

