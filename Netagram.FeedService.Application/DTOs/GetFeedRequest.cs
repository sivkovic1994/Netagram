namespace Netagram.FeedService.Application.DTOs
{
    public class GetFeedRequest
    {
        public string UserId { get; set; } = null!;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
