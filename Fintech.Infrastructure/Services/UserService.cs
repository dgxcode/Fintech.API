using Fintech.Domain.Entities;
using Fintech.Domain.Interfaces;
using Fintech.Infrastructure.Data;
using Fintech.Application.Interfaces;
using Fintech.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly FintechDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(FintechDbContext context, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterAsync(UserRegisterDto dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists)
                throw new Exception("E-mail já cadastrado");

            var user = new User(
                name: dto.Name,
                email: dto.Email,
                passwordHash: _passwordHasher.HashPassword(dto.Password)
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> LoginAsync(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            return _tokenService.GenerateToken(user);
        }
    }
}
