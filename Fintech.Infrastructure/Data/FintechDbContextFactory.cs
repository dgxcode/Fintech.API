
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Fintech.Infrastructure.Data
{
    public class FintechDbContextFactory : IDesignTimeDbContextFactory<FintechDbContext>
    {
        public FintechDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Fintech.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FintechDbContext>();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new FintechDbContext(optionsBuilder.Options);
        }
    }
}

