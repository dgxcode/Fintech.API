
namespace Fintech.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null;
        public string Email { get; private set; } = null;
        public string PasswordHash { get; private set; } = null;

        // Navigation property
        public Wallet Wallet { get; private set; } = null;

        // Required by EF Core
        public User() { }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Wallet = new Wallet(this); 
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Nome não pode estar vazio.");

            Name = newName;
        }
    }
}

