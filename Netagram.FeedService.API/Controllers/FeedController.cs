using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netagram.FeedService.Application.DTOs;
using Netagram.FeedService.Application.Interfaces;
using System.Security.Claims;

namespace Netagram.FeedService.API.Controllers
{
    [ApiController]
    [Route("feed")]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeed([FromQuery] GetFeedRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            request.UserId = userId;

            var result = await _feedService.GetFeedAsync(request);
            return Ok(result);
        }
    }
}
