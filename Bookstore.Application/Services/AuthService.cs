using Bookstore.Contract.Requests.User;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Interfaces.IServices;
using Bookstore.Domain.Validations.Auth;
using Bookstore.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bookstore.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _config;
        private readonly CreateUserRequestValidator _validations;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, 
            IConfiguration config, CreateUserRequestValidator validations, IHttpContextAccessor httpContextAccessor)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _config = config;
            _validations = validations;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> LoginAsync(LoginUserRequest request)
        {            
            var user = await _authRepository.GetUserByUsername(request.Username);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.HashPassword))
            {
                return "Username or password is incorrect";
            }            
            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(CreateUserRequest request)
        {
            var validationResult = _validations.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors[0].ErrorMessage);
            }

            var existingUser = await _authRepository.GetUserByUsername(request.Username);
            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Username = request.Username,
                Fullname = request.Fullname,
                HashPassword = _passwordHasher.HashPassword(request.Password)
            };

            await _authRepository.Register(user);
            return true;
        }
        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var currentUsername = GetCurrentUsername();
            var user = await _authRepository.GetUserByUsername(currentUsername);

            if (user == null)
            {                
                return false;
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                user.HashPassword = _passwordHasher.HashPassword(request.NewPassword);
            }

            if (!string.IsNullOrEmpty(request.Fullname))
            {
                user.Fullname = request.Fullname;
            }
            await _authRepository.UpdateUser(user);
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            claims.Add(new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "customer"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private string GetCurrentUsername()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var identity = httpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;
                    return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                }
            }
            return string.Empty;
        }

        private string GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var identity = httpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;
                    return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                }
            }
            return string.Empty;
        }

    }
}