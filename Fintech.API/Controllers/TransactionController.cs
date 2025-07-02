
using Fintech.Application.DTOs;
using Fintech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public TransactionController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        // GET: api/transactions/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactions(
            Guid userId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
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

        // POST: api/transactions/transfer
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
    }
}
