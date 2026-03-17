namespace Netagram.UserService.Domain.Entities
{
    public class UserFollow
    {
        public string FollowerId { get; set; } = default!;
        public string FollowingId { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
