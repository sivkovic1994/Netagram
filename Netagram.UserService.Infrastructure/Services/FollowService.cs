using Microsoft.EntityFrameworkCore;
using Netagram.UserService.Application.DTOs;
using Netagram.UserService.Application.Interfaces;
using Netagram.UserService.Domain.Entities;
using Netagram.UserService.Infrastructure.Persistence;

namespace Netagram.UserService.Infrastructure.Services
{
    public class FollowService : IFollowService
    {
        private readonly UserDbContext _context;

        public FollowService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserResult> FollowUserAsync(string followerId, string followingId)
        {
            if (followerId == followingId)
            {
                return new UserResult
                {
                    Success = false,
                    StatusCode = 400,
                    Errors = new { error = "You cannot follow yourself" }
                };
            }

            var exists = await _context.UserFollows
                .AnyAsync(uf => uf.FollowerId == followerId && uf.FollowingId == followingId);

            if (exists)
            {
                return new UserResult
                {
                    Success = false,
                    StatusCode = 400,
                    Errors = new { error = "Already following this user" }
                };
            }

            var follow = new UserFollow
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            _context.UserFollows.Add(follow);
            await _context.SaveChangesAsync();

            return new UserResult
            {
                Success = true,
                StatusCode = 200
            };
        }

        public async Task<UserResult> UnfollowUserAsync(string followerId, string followingId)
        {
            var follow = await _context.UserFollows
                .FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);

            if (follow == null)
            {
                return new UserResult
                {
                    Success = false,
                    StatusCode = 404
                };
            }

            _context.UserFollows.Remove(follow);
            await _context.SaveChangesAsync();

            return new UserResult
            {
                Success = true,
                StatusCode = 200
            };
        }

        public async Task<UserResult> GetFollowersAsync(string userId)
        {
            var followers = await _context.UserFollows
                .Where(x => x.FollowingId == userId)
                .Select(x => x.FollowerId)
                .ToListAsync();

            return new UserResult
            {
                Success = true,
                Data = followers
            };
        }

        public async Task<UserResult> GetFollowingAsync(string userId)
        {
            var following = await _context.UserFollows
                .Where(x => x.FollowerId == userId)
                .Select(x => x.FollowingId)
                .ToListAsync();

            return new UserResult
            {
                Success = true,
                Data = following
            };
        }
    }
}
