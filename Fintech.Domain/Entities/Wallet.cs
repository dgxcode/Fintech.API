
namespace Fintech.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Balance { get; private set; }

        // Navigation property
        public User User { get; private set; } = null;

        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

        // Required by EF Core
        public Wallet()
        {
        }

        public Wallet(User user)
        {
            Id = Guid.NewGuid();
            UserId = user?.Id ?? throw new ArgumentNullException(nameof(user));
            User = user;
            Balance = 0m;
            Transactions = new List<Transaction>();
        }

        public void AddBalance(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor deve ser positivo");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor deve ser positivo");

            Balance -= amount;
        }

        public void RegisterTransaction(Transaction transaction)
        {
            Transactions.Add(transaction ?? throw new ArgumentNullException(nameof(transaction)));
        }
    }
}
