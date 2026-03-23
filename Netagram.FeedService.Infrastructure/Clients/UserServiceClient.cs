using System.Net.Http.Json;

namespace Netagram.FeedService.Infrastructure.Clients
{
    // This is called a UserServiceClient because it is a client that calls the User Service.
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _http;

        public UserServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<string>> GetFollowingIdsAsync(string userId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<string>>($"users/{userId}/following") ?? new List<string>();
        }
    }
}
