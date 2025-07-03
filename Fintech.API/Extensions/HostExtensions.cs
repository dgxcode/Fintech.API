
using Fintech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fintech.API.Extensions
{
    /// <summary>
    /// Extensões para aplicar migrations e realizar o seeding do banco de dados
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// Aplica automaticamente as migrations e executa o seed de dados
        /// </summary>
        /// <param name="app">A aplicação host</param>
        public static void ApplyMigrationsAndSeedDatabase(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<FintechDbContext>();

                // aplica migrations pendentes
                context.Database.Migrate();

                // executa o seeding
                FintechDbContextSeed.SeedAsync(context).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // log se desejar
                Console.WriteLine($"Erro ao migrar/seed do banco de dados: {ex.Message}");
                throw;
            }
        }
    }
}
