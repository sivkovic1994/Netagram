using Microsoft.AspNetCore.Identity;
using System;

namespace Netagram.UserService.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
