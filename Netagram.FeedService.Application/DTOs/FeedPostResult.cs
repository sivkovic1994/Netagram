namespace Netagram.FeedService.Application.DTOs
{
    public class FeedPostResult
    {
        public Guid Id { get; set; }
        public string AuthorId { get; set; } = null!;
        public string AuthorUsername { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
