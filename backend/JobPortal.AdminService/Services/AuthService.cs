using JobPortal.AdminService.DTOs;
using JobPortal.AdminService.Models;
using JobPortal.AdminService.Repositories;

namespace JobPortal.AdminService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto.Email.ToLower() == "admin@gmail.com")
                return "This email is not allowed for registration";

            if (await _userRepository.EmailExistsAsync(registerDto.Email))
                return "Email already registered";

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "JobSeeker",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return "Registration successful";
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return "Invalid email or password";

            return _tokenService.GenerateToken(user);
        }
    }
}