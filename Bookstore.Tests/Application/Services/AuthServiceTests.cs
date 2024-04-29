using Bookstore.Application.Services;
using Bookstore.Contract.Requests.User;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Validations.Auth;
using Bookstore.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace Bookstore.Tests.Application.Services;
public class AuthServiceTests
{
    private readonly Mock<IAuthRepository> _mockAuthRepository;
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;
    private readonly IConfiguration _config;
    private readonly CreateUserRequestValidator _validations;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    public AuthServiceTests()
    {
        _mockAuthRepository = new Mock<IAuthRepository>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _config = new ConfigurationBuilder().AddInMemoryCollection().Build();
        _config["AppSettings:Token"] = "!@#%^&*()_+-=[]{};':\"|,.<>/?`~abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        _validations = new CreateUserRequestValidator();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _loggerMock = new Mock<ILogger<AuthService>>();
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsJwtToken()
    {
        // Arrange
        var username = "testuser";
        var password = "testpassword";
        var hashedPassword = "hashedpassword";
        var user = new User { Id = "1", Username = username, HashPassword = hashedPassword };

        _mockAuthRepository.Setup(x => x.GetUserByUsername(username)).ReturnsAsync(user);
        _mockPasswordHasher.Setup(x => x.VerifyPassword(password, hashedPassword)).Returns(true);

        var authService = new AuthService
                (_mockAuthRepository.Object, _mockPasswordHasher.Object,
                _config, _validations, _mockHttpContextAccessor.Object, _loggerMock.Object);
        var request = new LoginUserRequest(Username: username, Password: password);

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<string>(result);
    }

    [Fact]
    public async Task RegisterAsync_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var username = "testuser";
        var password = "testpassword";
        var fullname = "Test User";
        var hashedPassword = "hashedpassword";

        _mockPasswordHasher.Setup(x => x.HashPassword(password)).Returns(hashedPassword);
        _mockAuthRepository.Setup(x => x.GetUserByUsername(username)).ReturnsAsync((User)null);

        var authService = new AuthService
                (_mockAuthRepository.Object, _mockPasswordHasher.Object,
                _config, _validations, _mockHttpContextAccessor.Object, _loggerMock.Object);
        var request = new CreateUserRequest(Username: username, Password: password, Fullname: fullname);

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.True(result);
        _mockAuthRepository.Verify(x => x.Register(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var username = "testuser";
        var userId = "1";
        var newPassword = "newpassword";
        var newFullname = "New User Name";
        var hashedPassword = "hashedpassword";
        var user = new User { Id = userId, Username = username, HashPassword = hashedPassword };

        _mockAuthRepository.Setup(x => x.GetUserByUsername(username)).ReturnsAsync(user);
        _mockPasswordHasher.Setup(x => x.HashPassword(newPassword)).Returns(hashedPassword);
        SetupMockHttpContext(username, userId.ToString());

        var authService = new AuthService
                (_mockAuthRepository.Object, _mockPasswordHasher.Object,
                _config, _validations, _mockHttpContextAccessor.Object, _loggerMock.Object);
        var request = new UpdateUserRequest(NewPassword: newPassword, Fullname: newFullname);

        // Act
        var result = await authService.UpdateUserAsync(request);

        // Assert
        Assert.True(result);
        _mockAuthRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
    }

    private void SetupMockHttpContext(string username, string userId)
    {
        var identity = new GenericIdentity(username);
        var principal = new GenericPrincipal(identity, null);
        principal.AddIdentity(identity);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        identity.AddClaims(claims);

        var httpContext = new DefaultHttpContext
        {
            User = principal
        };

        _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);
    }
}