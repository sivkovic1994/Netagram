namespace Netagram.FeedService.Infrastructure.Clients
{
    public interface IUserServiceClient
    {
        Task<IEnumerable<string>> GetFollowingIdsAsync(string userId);
    }
}
