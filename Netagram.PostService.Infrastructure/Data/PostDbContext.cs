using Microsoft.EntityFrameworkCore;
using Netagram.PostService.Domain.Entities;

namespace Netagram.PostService.Infrastructure.Data
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}
