
using Fintech.Domain.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace Fintech.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<string> _hasher = new();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _hasher.VerifyHashedPassword(null!, passwordHash, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
