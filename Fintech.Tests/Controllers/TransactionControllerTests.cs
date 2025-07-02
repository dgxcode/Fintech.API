
using Xunit;
using Moq;
using Fintech.API.Controllers;
using Fintech.Application.Interfaces;
using Fintech.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Tests.Controllers
{
    public class TransactionControllerTests
    {
        [Fact]
        public async Task GetTransactions_ReturnsOk_WithTransactions()
        {
            // arrange
            var walletServiceMock = new Mock<IWalletService>();
            var userId = Guid.NewGuid();

            walletServiceMock
                .Setup(x => x.GetTransactionsAsync(userId, null, null))
                .ReturnsAsync(new List<TransactionListDto>
                {
                    new TransactionListDto
                    {
                        TransactionId = Guid.NewGuid(),
                        FromUserId = userId,
                        ToUserId = Guid.NewGuid(),
                        Amount = 100,
                        Timestamp = DateTime.UtcNow
                    }
                });

            var controller = new TransactionController(walletServiceMock.Object);

            // act
            var result = await controller.GetTransactions(userId, null, null);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var transactions = Assert.IsAssignableFrom<List<TransactionListDto>>(okResult.Value);
            Assert.Single(transactions);
        }
    }
}
