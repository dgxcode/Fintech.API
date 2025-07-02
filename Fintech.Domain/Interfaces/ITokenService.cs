
using Fintech.Domain.Entities;

namespace Fintech.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
