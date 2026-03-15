using Netagram.UserService.Application.DTOs;

namespace Netagram.UserService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterRequest request);
        Task<AuthResult> LoginAsync(LoginRequest request);
        Task<AuthResult> GetCurrentUserAsync(string userId);
    }
}
