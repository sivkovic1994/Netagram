using Netagram.FeedService.Application.DTOs;

namespace Netagram.FeedService.Application.Interfaces
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedPostResult>> GetFeedAsync(GetFeedRequest request);
    }
}
