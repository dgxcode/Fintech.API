
using Fintech.Domain.Entities;

namespace Fintech.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<List<Transaction>> GetByUserIdAsync(Guid userId, DateTime? startDate, DateTime? endDate);
    }
}
