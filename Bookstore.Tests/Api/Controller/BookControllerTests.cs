using Bookstore.Api.Controllers;
using Bookstore.Contract.Requests.Book;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookstoreTests.Controllers
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();
            _httpContextMock = new Mock<HttpContext>();
            _httpContextMock.SetupGet(c => c.User.Identity.IsAuthenticated).Returns(true);
            //_httpContextMock.SetupGet(c => c.User.IsInRole("admin")).Returns(true);
            _controller = new BooksController(_bookServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public async Task CreateBook_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var request = new CreateBookRequest 
                ( Title: "Test Book", 
                Author :"Test Author", 
                Description: "Description",
                GenreId :"1", 
                Price: 9.99 );
            var bookResponse = new BookResponse("1", "Test Book", "Test Author", "Description", "Fiction", 9.99);
            _bookServiceMock.Setup(s => s.AddBookAsync(request)).ReturnsAsync(bookResponse);

            // Act
            var result = await _controller.CreateBook(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(bookResponse, okResult.Value);
        }

        [Fact]
        public async Task UpdateBook_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var bookId = "1";
            var request = new UpdateBookRequest
                (Title: "Updated Book",
                Author: "Updated Author",
                Description: "Description",
                GenreId: "1",
                Price: 12.99);
            var bookResponse = new BookResponse(bookId, "Updated Book", "Updated Author", "Description", "Mystery", 12.99);
            _bookServiceMock.Setup(s => s.UpdateBookAsync(bookId, request)).ReturnsAsync(bookResponse);

            // Act
            var result = await _controller.UpdateBook(bookId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(bookResponse, okResult.Value);
        }

        [Fact]
        public async Task DeleteBook_ValidId_ShouldReturnNoContent()
        {
            // Arrange
            var bookId = "1";
            _bookServiceMock.Setup(s => s.DeleteBookByIdAsync(bookId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBook(bookId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


    }
}