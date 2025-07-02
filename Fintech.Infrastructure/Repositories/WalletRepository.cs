using Fintech.Infrastructure.Data;
using Fintech.Domain.Entities;
using Fintech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly FintechDbContext _context;

        public WalletRepository(FintechDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Wallets
                .Include(w => w.Transactions)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
