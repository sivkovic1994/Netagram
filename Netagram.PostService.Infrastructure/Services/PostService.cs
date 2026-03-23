using Microsoft.EntityFrameworkCore;
using Netagram.PostService.Application;
using Netagram.PostService.Application.DTOs;
using Netagram.PostService.Domain.Entities;
using Netagram.PostService.Infrastructure.Data;

namespace Netagram.PostService.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly PostDbContext _context;

        public PostService(PostDbContext context)
        {
            _context = context;
        }

        public async Task<PostResult> CreatePostAsync(string userId, string authorUsername, CreatePostRequest request)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AuthorUsername = authorUsername,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return new PostResult
            {
                Id = post.Id,
                Content = post.Content,
                AuthorId = userId,
                AuthorUsername = post.AuthorUsername,
                CreatedAt = post.CreatedAt,
                Likes = 0,
                Comments = 0
            };
        }

        public async Task<PostResult?> GetPostByIdAsync(Guid postId)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .Select(p => new PostResult
                {
                    Id = p.Id,
                    Content = p.Content,
                    AuthorId = p.UserId,
                    AuthorUsername = p.AuthorUsername,
                    CreatedAt = p.CreatedAt,
                    Likes = 0,
                    Comments = 0
                })
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<IEnumerable<PostResult>> GetPostsByUserIdsAsync(IEnumerable<string> userIds)
        {
            if (userIds == null)
                return Enumerable.Empty<PostResult>();

            var idsList = userIds as IList<string> ?? userIds.ToList();
            if (!idsList.Any())
                return Enumerable.Empty<PostResult>();

            return await _context.Posts
                .Where(p => idsList.Contains(p.UserId))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PostResult
                {
                    Id = p.Id,
                    Content = p.Content,
                    AuthorId = p.UserId,
                    AuthorUsername = p.AuthorUsername,
                    CreatedAt = p.CreatedAt,
                    Likes = 0,
                    Comments = 0
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PostResult>> GetUserPostsAsync(string userId)
        {
            return await _context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PostResult
                {
                    Id = p.Id,
                    Content = p.Content,
                    AuthorId = p.UserId,
                    AuthorUsername = p.AuthorUsername,
                    CreatedAt = p.CreatedAt,
                    Likes = 0,
                    Comments = 0
                })
                .ToListAsync();
        }

        public async Task<bool> DeletePostAsync(string userId, Guid postId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null || post.UserId != userId)
                return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
