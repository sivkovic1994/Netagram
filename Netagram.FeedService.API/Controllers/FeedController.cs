using Microsoft.AspNetCore.Mvc;
using Netagram.FeedService.Application.DTOs;
using Netagram.FeedService.Application.Interfaces;

namespace Netagram.FeedService.API.Controllers
{
    [ApiController]
    [Route("feed")]
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
            var result = await _feedService.GetFeedAsync(request);
            return Ok(result);
        }
    }
}
