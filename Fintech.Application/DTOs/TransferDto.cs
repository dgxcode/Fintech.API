
namespace Fintech.Application.DTOs
{
    public class TransferDto
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
