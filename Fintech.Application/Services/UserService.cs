
using Fintech.Application.DTOs;
using Fintech.Application.Interfaces;
using Fintech.Domain.Entities;
using Fintech.Domain.Interfaces;

namespace Fintech.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService
        )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(UserRegisterDto userDto)
        {
            var existing = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existing != null)
                throw new InvalidOperationException("Email já cadastrado.");

            var hash = _passwordHasher.HashPassword(userDto.Password);

            var user = new User(userDto.Name, userDto.Email, hash);

            await _userRepository.AddAsync(user);
        }

        public async Task<string> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            var isValid = _passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash);

            if (!isValid)
                throw new InvalidOperationException("Senha inválida.");

            return _tokenService.GenerateToken(user);
        }
    }
}
