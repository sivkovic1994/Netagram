using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Netagram.UserService.Domain.Entities;

namespace Netagram.UserService.Infrastructure.Data;

public class UserDbContext : IdentityDbContext<ApplicationUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserFollow> UserFollows { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Prevents duplicate follows by setting a composite key on FollowerId and FollowingId
        builder.Entity<UserFollow>(entity =>
        {
            entity.HasKey(uf => new { uf.FollowerId, uf.FollowingId });
        });
    }
}
