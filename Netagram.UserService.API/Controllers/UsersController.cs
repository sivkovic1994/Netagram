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
        private readonly IFollowService _followService;

        public UsersController(IUserService userService, IFollowService followService)
        {
            _userService = userService;
            _followService = followService;
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

        [Authorize]
        [HttpPost("{id}/follow")]
        public async Task<IActionResult> FollowUser(string id)
        {
            var followerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _followService.FollowUserAsync(followerId!, id);

            if (!result.Success)
                return StatusCode(result.StatusCode, result.Errors);

            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/unfollow")]
        public async Task<IActionResult> UnfollowUser(string id)
        {
            var followerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _followService.UnfollowUserAsync(followerId!, id);

            if (!result.Success)
                return StatusCode(result.StatusCode);

            return Ok();
        }

        [HttpGet("{id}/followers")]
        public async Task<IActionResult> GetFollowers(string id)
        {
            var result = await _followService.GetFollowersAsync(id);

            return Ok(result.Data);
        }

        [HttpGet("{id}/following")]
        public async Task<IActionResult> GetFollowing(string id)
        {
            var result = await _followService.GetFollowingAsync(id);

            return Ok(result.Data);
        }
    }
}
