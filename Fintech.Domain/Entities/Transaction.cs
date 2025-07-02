
namespace Fintech.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }

        public Guid FromWalletId { get; private set; }
        public Guid ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Timestamp { get; private set; }

        // Navigation properties
        public Wallet FromWallet { get; private set; }
        public Wallet ToWallet { get; private set; }

        // Required by EF Core
        private Transaction() { }

        public Transaction(Guid fromWalletId, Guid toWalletId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor deve ser positivo");

            Id = Guid.NewGuid();
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }
    }
}
