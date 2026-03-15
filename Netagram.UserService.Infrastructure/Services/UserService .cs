using Microsoft.AspNetCore.Identity;
using Netagram.UserService.Application.DTOs;
using Netagram.UserService.Application.Interfaces;
using Netagram.UserService.Infrastructure.Data;

namespace Netagram.UserService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResult> GetCurrentUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new UserResult
                {
                    Success = false,
                    StatusCode = 404
                };
            }

            return new UserResult
            {
                Success = true,
                Data = new UserResponse
                {
                    UserId = user.Id,
                    Email = user.Email!,
                    Username = user.UserName!
                }
            };
        }

        public async Task<UserResult> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new UserResult
                {
                    Success = false,
                    StatusCode = 404
                };
            }

            return new UserResult
            {
                Success = true,
                Data = new UserResponse
                {
                    UserId = user.Id,
                    Email = user.Email!,
                    Username = user.UserName!
                }
            };
        }
    }
}
