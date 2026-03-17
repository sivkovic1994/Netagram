using Netagram.UserService.Application.DTOs;

namespace Netagram.UserService.Application.Interfaces
{
    public interface IFollowService
    {
        Task<UserResult> FollowUserAsync(string followerId, string followingId);
        Task<UserResult> UnfollowUserAsync(string followerId, string followingId);
        Task<UserResult> GetFollowersAsync(string userId);
        Task<UserResult> GetFollowingAsync(string userId);
    }
}
