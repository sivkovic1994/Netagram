using Microsoft.EntityFrameworkCore;
using Netagram.FeedService.Domain.Entities;

namespace Netagram.FeedService.Infrastructure.Data
{
    public class FeedDbContext : DbContext
    {
        public FeedDbContext(DbContextOptions<FeedDbContext> options) : base(options) { }

        public DbSet<FeedItem> FeedItems { get; set; } = null!;
    }
}
