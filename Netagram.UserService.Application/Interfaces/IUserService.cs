using Netagram.UserService.Application.DTOs;

namespace Netagram.UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> GetCurrentUserAsync(string userId);
        Task<UserResult> GetUserByIdAsync(string userId);
    }
}
