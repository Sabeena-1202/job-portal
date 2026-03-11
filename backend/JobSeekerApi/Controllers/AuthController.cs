using Microsoft.AspNetCore.Mvc;
using JobSeekerApi.DTOs;
using JobSeekerApi.Services;

namespace JobSeekerApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(dto);

            if (result == null)
                return BadRequest(new { message = "Email already exists!" });

            return Ok(result);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(dto);

            if (result == null)
                return Unauthorized(new { message = "Invalid email or password!" });

            return Ok(result);
        }
    }
}