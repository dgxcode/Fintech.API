using Fintech.Application.DTOs;
using Fintech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        // GET: api/wallet/{userId}/balance
        [HttpGet("{userId}/balance")]
        public async Task<IActionResult> GetBalance(Guid userId)
        {
            try
            {
                var result = await _walletService.GetWalletBalanceAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/wallet/{userId}/deposit
        [HttpPost("{userId}/deposit")]
        public async Task<IActionResult> Deposit(Guid userId, [FromBody] DepositDto dto)
        {
            try
            {
                if (userId != dto.UserId)
                    return BadRequest(new { error = "UserId no corpo não corresponde ao UserId da rota." });

                await _walletService.DepositAsync(dto);
                return Ok(new { message = "Depósito realizado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/wallet/transfer
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto dto)
        {
            try
            {
                await _walletService.TransferAsync(dto);
                return Ok(new { message = "Transferência realizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/wallet/{userId}/transactions
        [HttpGet("{userId}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var transactions = await _walletService.GetTransactionsAsync(userId, startDate, endDate);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
