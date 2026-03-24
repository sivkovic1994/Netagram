using Netagram.FeedService.Application.DTOs;
using Netagram.FeedService.Application.Interfaces;
using Netagram.FeedService.Infrastructure.Clients;

namespace Netagram.FeedService.Infrastructure.Services
{
    public class FeedService : IFeedService
    {
        private readonly IUserServiceClient _userClient;
        private readonly IPostServiceClient _postClient;

        public FeedService(
            IUserServiceClient userClient,
            IPostServiceClient postClient)
        {
            _userClient = userClient;
            _postClient = postClient;
        }

        public async Task<IEnumerable<FeedPostResult>> GetFeedAsync(GetFeedRequest request)
        {
            var followingIds = await _userClient.GetFollowingIdsAsync(request.UserId);

            var posts = await _postClient.GetPostsByUserIdsAsync(followingIds);

            var result = posts
                .OrderByDescending(p => p.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new FeedPostResult
                {
                    Id = p.Id,
                    Content = p.Content,
                    AuthorId = p.AuthorId,
                    AuthorUsername = p.AuthorUsername,
                    CreatedAt = p.CreatedAt
                });

            return result;
        }
    }
}
