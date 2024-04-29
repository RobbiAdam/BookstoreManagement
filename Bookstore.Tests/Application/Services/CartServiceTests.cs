using Bookstore.Application.Services;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Moq;
using Xunit;

namespace BookstoreTests.Services
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _cartService = new CartService(_cartRepositoryMock.Object, _bookRepositoryMock.Object);
        }

        [Fact]
        public async Task AddToCart_ValidBookId_ShouldAddToCart()
        {
            // Arrange
            var userId = "user1";
            var bookId = "book1";
            var book = new Book { Id = bookId, Title = "Test Book" };
            _bookRepositoryMock.Setup(r => r.GetBookById(bookId)).ReturnsAsync(book);

            // Act
            await _cartService.AddToCart(userId, bookId);

            // Assert
            _cartRepositoryMock.Verify(r => r.AddToCart(It.IsAny<Cart>()), Times.Once);
        }

        [Fact]
        public async Task RemoveFromCart_ValidBookId_ShouldRemoveFromCart()
        {
            // Arrange
            var userId = "user1";
            var bookId = "book1";
            var cart = new Cart { UserId = userId, CartItems = new List<CartItem> { new CartItem { BookId = bookId } } };
            _cartRepositoryMock.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);

            // Act
            await _cartService.RemoveFromCart(userId, bookId);

            // Assert
            _cartRepositoryMock.Verify(r => r.UpdateCart(It.IsAny<Cart>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItemQuantity_ValidBookId_ShouldUpdateQuantity()
        {
            // Arrange
            var userId = "user1";
            var bookId = "book1";
            var quantity = 3;
            var cart = new Cart { UserId = userId, CartItems = new List<CartItem> { new CartItem { BookId = bookId, Quantity = 1 } } };
            _cartRepositoryMock.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);

            // Act
            await _cartService.UpdateCartItemQuantity(userId, bookId, quantity);

            // Assert
            _cartRepositoryMock.Verify(r => r.UpdateCart(It.Is<Cart>(c => c.CartItems.Any(i => i.BookId == bookId && i.Quantity == quantity))), Times.Once);
        }

        [Fact]
        public async Task GetCart_ValidUserId_ShouldReturnCart()
        {
            // Arrange
            var userId = "user1";
            var cart = new Cart { UserId = userId };
            _cartRepositoryMock.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);

            // Act
            var result = await _cartService.GetCart(userId);

            // Assert
            Assert.Equal(cart, result);
        }

        [Fact]
        public async Task ClearCart_ValidUserId_ShouldClearCart()
        {
            // Arrange
            var userId = "user1";
            var cart = new Cart { UserId = userId, CartItems = new List<CartItem> { new CartItem(), new CartItem() } };
            _cartRepositoryMock.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);

            // Act
            await _cartService.ClearCart(userId);

            // Assert
            _cartRepositoryMock.Verify(r => r.UpdateCart(It.Is<Cart>(c => c.CartItems.Count == 0)), Times.Once);
        }
    }
}