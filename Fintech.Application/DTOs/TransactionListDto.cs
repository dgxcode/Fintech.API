
using System;

namespace Fintech.Application.DTOs
{
    public class TransactionListDto
    {
        public Guid TransactionId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
