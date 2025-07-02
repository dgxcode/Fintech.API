using Fintech.API.Controllers;
using Fintech.Application.DTOs;
using Fintech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fintech.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenUserIsRegistered()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "123456"
            };

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Usuário registrado com sucesso", okResult.Value.ToString());
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "123456"
            };

            _userServiceMock.Setup(x => x.RegisterAsync(dto))
                .ThrowsAsync(new Exception("Erro ao registrar"));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Erro ao registrar", badRequest.Value.ToString());
        }

        [Fact]
        public async Task Login_ReturnsOk_WithToken()
        {
            // Arrange
            var dto = new UserLoginDto
            {
                Email = "test@example.com",
                Password = "123456"
            };

            _userServiceMock.Setup(x => x.LoginAsync(dto))
                .ReturnsAsync("mocked_token");

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("mocked_token", okResult.Value.ToString());
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenInvalidCredentials()
        {
            // Arrange
            var dto = new UserLoginDto
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };

            _userServiceMock.Setup(x => x.LoginAsync(dto))
                .ReturnsAsync((string?)null);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Contains("inválidos", unauthorized.Value.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var dto = new UserLoginDto
            {
                Email = "test@example.com",
                Password = "123456"
            };

            _userServiceMock.Setup(x => x.LoginAsync(dto))
                .ThrowsAsync(new Exception("Erro interno"));

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Erro interno", badRequest.Value.ToString());
        }
    }
}
