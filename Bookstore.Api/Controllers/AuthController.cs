using Bookstore.Contract.Requests.User;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var registrationResult = await _authService.RegisterAsync(request);
            if (!registrationResult)
            {
                return BadRequest("Username already exists");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
            {
                return Unauthorized("Username or password is incorrect");
            }

            return Ok(token);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserRequest request)
        {
            var result = await _authService.UpdateUserAsync(request);

            if (result)
            {
                return Ok("User profile updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update user profile.");
            }
        }
    }
}