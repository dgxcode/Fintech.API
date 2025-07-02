using Fintech.Domain.Entities;
using Fintech.Domain.Interfaces;
using Fintech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FintechDbContext _context;

        public UserRepository(FintechDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Wallet) 
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Wallet)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}










/////old
//using Fintech.Infrastructure.Data;
//using Fintech.Domain.Entities;
//using Fintech.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Fintech.Infrastructure.Repositories
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly FintechDbContext _context;

//        public UserRepository(FintechDbContext context)
//        {
//            _context = context;
//        }

//        public async Task AddAsync(User user)
//        {
//            await _context.Users.AddAsync(user);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<User?> GetByEmailAsync(string email)
//        {
//            return await _context.Users
//                .Include(u => u.Wallet)
//                .FirstOrDefaultAsync(u => u.Email == email);
//        }

//        public async Task<User?> GetByIdAsync(Guid userId)
//        {
//            return await _context.Users
//                .Include(u => u.Wallet)
//                .FirstOrDefaultAsync(u => u.Id == userId);
//        }
//    }
//}
