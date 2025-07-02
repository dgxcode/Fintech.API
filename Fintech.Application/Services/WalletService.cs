using Fintech.Application.DTOs;
using Fintech.Application.Interfaces;
using Fintech.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fintech.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletService(
            IWalletRepository walletRepository,
            ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<WalletBalanceDto> GetWalletBalanceAsync(Guid userId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);
            if (wallet == null)
                throw new InvalidOperationException("Carteira não encontrada.");

            return new WalletBalanceDto
            {
                UserId = userId,
                Balance = wallet.Balance
            };
        }

        public async Task DepositAsync(DepositDto depositDto)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(depositDto.UserId);
            if (wallet == null)
                throw new InvalidOperationException("Carteira não encontrada.");

            wallet.AddBalance(depositDto.Amount);

            await _walletRepository.UpdateAsync(wallet);
        }

        public async Task TransferAsync(TransferDto transferDto)
        {
            var fromWallet = await _walletRepository.GetByUserIdAsync(transferDto.FromUserId);
            var toWallet = await _walletRepository.GetByUserIdAsync(transferDto.ToUserId);

            if (fromWallet == null)
                throw new InvalidOperationException("Carteira de origem não encontrada.");
            if (toWallet == null)
                throw new InvalidOperationException("Carteira de destino não encontrada.");

            fromWallet.Withdraw(transferDto.Amount);
            toWallet.AddBalance(transferDto.Amount);

            var transaction = new Domain.Entities.Transaction(
                fromWallet.Id,
                toWallet.Id,
                transferDto.Amount
            );

            fromWallet.RegisterTransaction(transaction);
            toWallet.RegisterTransaction(transaction);

            await _transactionRepository.AddAsync(transaction);
            await _walletRepository.UpdateAsync(fromWallet);
            await _walletRepository.UpdateAsync(toWallet);
        }

        public async Task<List<TransactionListDto>> GetTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId, startDate, endDate);

            var result = new List<TransactionListDto>();

            foreach (var tx in transactions)
            {
                result.Add(new TransactionListDto
                {
                    TransactionId = tx.Id,
                    FromUserId = tx.FromWallet.User.Id,
                    ToUserId = tx.ToWallet.User.Id,
                    Amount = tx.Amount,
                    Timestamp = tx.Timestamp
                });
            }

            return result;
        }
    }
}

