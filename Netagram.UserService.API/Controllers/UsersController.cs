using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netagram.UserService.Application.Interfaces;
using System.Security.Claims;

namespace Netagram.UserService.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _userService.GetCurrentUserAsync(userId);

            if (!result.Success)
                return StatusCode(result.StatusCode);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (!result.Success)
                return StatusCode(result.StatusCode);

            return Ok(result.Data);
        }
    }
}
