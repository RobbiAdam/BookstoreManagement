using Bookstore.Api.Controllers;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookstoreTests.Controllers
{
    public class CartsControllerTests
    {
        private readonly Mock<ICartService> _cartServiceMock;
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly CartsController _controller;

        public CartsControllerTests()
        {
            _cartServiceMock = new Mock<ICartService>();
            _httpContextMock = new Mock<HttpContext>();
            _httpContextMock.SetupGet(c => c.User.Identity.Name).Returns("testuser");

            _controller = new CartsController(_cartServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public async Task AddToCart_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var bookId = "book1";

            // Act
            var result = await _controller.AddToCart(bookId);

            // Assert
            _cartServiceMock.Verify(s => s.AddToCart("testuser", bookId), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task RemoveFromCart_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var bookId = "book1";

            // Act
            var result = await _controller.RemoveFromCart(bookId);

            // Assert
            _cartServiceMock.Verify(s => s.RemoveFromCart("testuser", bookId), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateCartItemQuantity_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var bookId = "book1";
            var quantity = 3;

            // Act
            var result = await _controller.UpdateCartItemQuantity(bookId, quantity);

            // Assert
            _cartServiceMock.Verify(s => s.UpdateCartItemQuantity("testuser", bookId, quantity), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetCart_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var cart = new Cart { UserId = "testuser", CartItems = new List<CartItem>() };
            _cartServiceMock.Setup(s => s.GetCart("testuser")).ReturnsAsync(cart);

            // Act
            var result = await _controller.GetCart();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(cart, okResult.Value);
        }

        [Fact]
        public async Task ClearCart_ValidRequest_ShouldReturnOk()
        {
            // Act
            var result = await _controller.ClearCart();

            // Assert
            _cartServiceMock.Verify(s => s.ClearCart("testuser"), Times.Once);
            Assert.IsType<OkResult>(result);
        }
    }
}