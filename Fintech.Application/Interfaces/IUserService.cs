using Fintech.Application.DTOs;

namespace Fintech.Application.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterDto userDto);
        Task<string?> LoginAsync(UserLoginDto loginDto);
    }
}


