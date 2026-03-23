using Netagram.FeedService.Application.DTOs;

namespace Netagram.FeedService.Infrastructure.Clients
{
    public interface IPostServiceClient
    {
        Task<IEnumerable<PostDto>> GetPostsByUserIdsAsync(IEnumerable<string> userIds);
    }
}
