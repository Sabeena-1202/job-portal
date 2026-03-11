using JobPortal.AdminService.DTOs;

namespace JobPortal.AdminService.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}