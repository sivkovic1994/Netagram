using Netagram.PostService.Application.DTOs;

namespace Netagram.PostService.Application
{
    public interface IPostService
    {
        Task<PostResult> CreatePostAsync(string userId, string authorUsername, CreatePostRequest request);
        Task<PostResult?> GetPostByIdAsync(Guid postId);
        Task<IEnumerable<PostResult>> GetPostsByUserIdsAsync(IEnumerable<string> userIds);
        Task<IEnumerable<PostResult>> GetUserPostsAsync(string userId);
        Task<bool> DeletePostAsync(string userId, Guid postId);
    }
}
