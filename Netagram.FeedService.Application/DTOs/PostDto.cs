namespace Netagram.FeedService.Application.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
