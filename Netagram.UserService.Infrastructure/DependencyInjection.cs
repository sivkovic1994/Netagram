using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netagram.UserService.Application.Interfaces;
using Netagram.UserService.Infrastructure.Data;
using Netagram.UserService.Infrastructure.Services;

namespace Netagram.UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, Services.UserService>();
            services.AddScoped<IFollowService, FollowService>();

            return services;
        }
    }
}
