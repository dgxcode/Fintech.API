using Fintech.Application.DTOs;
using Fintech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fintech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            try
            {
                await _userService.RegisterAsync(userDto);
                return Ok(new { message = "Usuário registrado com sucesso." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var token = await _userService.LoginAsync(loginDto);
                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new { error = "Email ou senha inválidos." });

                return Ok(new { token });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
