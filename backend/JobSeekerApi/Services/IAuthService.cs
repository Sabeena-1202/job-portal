using JobSeekerApi.DTOs;

namespace JobSeekerApi.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> Register(RegisterDTO registerDTO);
        Task<AuthResponseDTO?> Login(LoginDTO loginDTO);
    }
}