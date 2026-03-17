namespace Netagram.PostService.Application.DTOs
{
    public class PostResult
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string AuthorUsername { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }
}
