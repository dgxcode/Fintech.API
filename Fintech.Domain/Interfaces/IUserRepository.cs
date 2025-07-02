using Fintech.Domain.Entities;

namespace Fintech.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
    }
}
