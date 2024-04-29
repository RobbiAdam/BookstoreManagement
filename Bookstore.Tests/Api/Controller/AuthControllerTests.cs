using Bookstore.Api.Controllers;
using Bookstore.Contract.Requests.User;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookstoreTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;
        private readonly Mock<HttpContext> _httpContextMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _httpContextMock = new Mock<HttpContext>();
            _httpContextMock.SetupGet(c => c.User.Identity.Name).Returns("testuser");
            _controller = new AuthController(_authServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public async Task Register_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var request = new CreateUserRequest
            (Username: "testuser", Fullname: "Test User", Password: "password");
            _authServiceMock.Setup(s => s.RegisterAsync(request)).ReturnsAsync(true);

            // Act
            var result = await _controller.Register(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Register_InvalidRequest_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateUserRequest
            (Username: "testuser", Fullname: "", Password: "password");
            _controller.ModelState.AddModelError("Fullname", "Fullname is required");

            // Act
            var result = await _controller.Register(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var request = new LoginUserRequest(Username: "testuser", Password: "password");
            var token = "validToken";
            _authServiceMock.Setup(s => s.LoginAsync(request)).ReturnsAsync(token);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(token, okResult.Value);
        }

        [Fact]
        public async Task UpdateUserProfile_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var request = new UpdateUserRequest ( NewPassword: "newpassword", Fullname:"Updated Name" );
            _authServiceMock.Setup(s => s.UpdateUserAsync(request)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateUserProfile(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User profile updated successfully.", okResult.Value);
        }

        [Fact]
        public async Task UpdateUserProfile_InvalidRequest_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new UpdateUserRequest (NewPassword:"", Fullname: "" );
            _controller.ModelState.AddModelError("NewPassword", "New password is required");

            // Act
            var result = await _controller.UpdateUserProfile(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}