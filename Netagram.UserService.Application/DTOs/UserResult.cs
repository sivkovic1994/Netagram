namespace Netagram.UserService.Application.DTOs
{
    public class UserResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public object? Errors { get; set; }
        public object? Data { get; set; }
    }
}
