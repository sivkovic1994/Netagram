namespace Netagram.PostService.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string AuthorUsername { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
