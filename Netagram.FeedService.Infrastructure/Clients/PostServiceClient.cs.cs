using Netagram.FeedService.Application.DTOs;
using System.Net.Http.Json;

namespace Netagram.FeedService.Infrastructure.Clients
{
    // This is called a PostServiceClient because it is a client that calls the Post Service.
    public class PostServiceClient : IPostServiceClient
    {
        private readonly HttpClient _http;

        public PostServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserIdsAsync(IEnumerable<string> userIds)
        {
            var response = await _http.PostAsJsonAsync("posts/users", userIds);

            return await response.Content.ReadFromJsonAsync<IEnumerable<PostDto>>() ?? new List<PostDto>();
        }
    }
}
