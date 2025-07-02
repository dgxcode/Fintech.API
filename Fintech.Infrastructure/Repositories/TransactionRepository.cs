using Fintech.Infrastructure.Data;
using Fintech.Domain.Entities;
using Fintech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FintechDbContext _context;

        public TransactionRepository(FintechDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetByUserIdAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions
                .Include(t => t.FromWallet)
                .ThenInclude(w => w.User)
                .Include(t => t.ToWallet)
                .ThenInclude(w => w.User)
                .AsQueryable();


            if (startDate.HasValue)
                query = query.Where(t => t.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Timestamp <= endDate.Value);

            query = query.Where(t =>
                t.FromWallet.UserId == userId ||
                t.ToWallet.UserId == userId);

            return await query.ToListAsync();
        }
    }
}
