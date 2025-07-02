using Fintech.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fintech.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletBalanceDto> GetWalletBalanceAsync(Guid userId);
        Task DepositAsync(DepositDto depositDto);
        Task TransferAsync(TransferDto transferDto);
        Task<List<TransactionListDto>> GetTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate);
    }
}
