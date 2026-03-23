using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netagram.PostService.Application;
using Netagram.PostService.Application.DTOs;
using System.Security.Claims;

namespace Netagram.PostService.API.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);

            var result = await _postService.CreatePostAsync(userId!, username!, request);

            return Ok(result);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetById(Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null) 
                return NotFound();

            return Ok(post);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var posts = await _postService.GetUserPostsAsync(userId!);

            return Ok(posts);
        }

        [HttpPost("users")]
        public async Task<IActionResult> GetPostsByUsers([FromBody] List<string> userIds)
        {
            var posts = await _postService.GetPostsByUserIdsAsync(userIds);
            return Ok(posts);
        }

        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete(Guid postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var success = await _postService.DeletePostAsync(userId!, postId);

            if (!success) 
                return Forbid();

            return NoContent();
        }
    }
}
