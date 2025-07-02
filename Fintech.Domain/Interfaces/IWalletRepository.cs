using Fintech.Domain.Entities;

namespace Fintech.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Wallet wallet);
    }
}
