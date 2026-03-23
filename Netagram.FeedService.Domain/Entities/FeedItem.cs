namespace Netagram.FeedService.Domain.Entities
{
    public class FeedItem
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public Guid PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
