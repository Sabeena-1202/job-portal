using JobPortal.AdminService.Models;

namespace JobPortal.AdminService.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}