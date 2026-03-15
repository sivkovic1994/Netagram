using Microsoft.AspNetCore.Mvc;
using Netagram.UserService.Application.DTOs;
using Netagram.UserService.Application.Interfaces;

namespace Netagram.UserService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
                return StatusCode(result.StatusCode, result.Errors);

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return StatusCode(result.StatusCode, result.Errors);

            return Ok(result.Data);
        }
    }
}
