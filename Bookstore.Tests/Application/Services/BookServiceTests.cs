using Bookstore.Application.Services;
using Bookstore.Contract.Requests.Book;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Validations.Books;
using Moq;
using Xunit;

namespace Bookstore.Tests.Application.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly CreateBookRequestValidator _createValidator;
        private readonly UpdateBookRequestValidator _updateBookRequestValidatorMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _createValidator = new CreateBookRequestValidator();
            _updateBookRequestValidatorMock = new UpdateBookRequestValidator();
            _bookService = new BookService(_bookRepositoryMock.Object, _genreRepositoryMock.Object,
                _createValidator, _updateBookRequestValidatorMock);
        }

        [Fact]
        public async Task AddBookAsync_ShouldAddBook()
        {
            // Arrange
            var existingGenre = new Genre { Id = "1", Name = "Fantasy" };
            var createBookRequest = new CreateBookRequest(
                Title: "Title",
                GenreId: existingGenre.Id,
                Description: "Description",
                Author: "Author",
                Price: 9.99
            );

            _bookRepositoryMock.Setup(repo => repo.AddBook(It.IsAny<Book>()))
                             .Returns(Task.CompletedTask);

            _genreRepositoryMock.Setup(repo => repo.GetGenreByIdAsync(existingGenre.Id))
                              .ReturnsAsync(existingGenre);

            // Act
            var result = await _bookService.AddBookAsync(createBookRequest);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetAllBooksAsync_ReturnsListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = "book-1", Title = "Book 1", Author = "Author 1", GenreId = "genre-1" },
                new Book { Id = "book-2", Title = "Book 2", Author = "Author 2", GenreId = "genre-2" }
            };

            _bookRepositoryMock.Setup(x => x.GetAllBooks()).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsBook()
        {
            // Arrange
            var book = new Book
            {
                Id = "book-1",
                Title = "Book 1",
                Author = "Author 1",
            };

            _bookRepositoryMock.Setup(x => x.GetBookById(It.IsAny<string>())).ReturnsAsync(book);
            // Act
            var result = await _bookService.GetBookByIdAsync("book-1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);
            Assert.Equal("Author 1", result.Author);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldUpdateBook()
        {
            // Arrange
            var existingBook = new Book { Id = "1", Title = "Existing Book", Description = "Existing Description", Author = "Existing Author", GenreId = "1", Price = 19.99 }; // Example existing book data
            var updatedBookRequest = new UpdateBookRequest(
                Title: "Updated Title",
                Description: "Updated Description",
                Author: "Updated Author",
                GenreId: "2", // New genre ID
                Price: 24.99 // New price
            );

            _bookRepositoryMock.Setup(repo => repo.GetBookById(existingBook.Id))
                             .ReturnsAsync(existingBook);

            _genreRepositoryMock.Setup(repo => repo.GetGenreByIdAsync(updatedBookRequest.GenreId))
                              .ReturnsAsync(new Genre { Id = "2", Name = "New Genre" });

            // Act
            var result = await _bookService.UpdateBookAsync(existingBook.Id, updatedBookRequest);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.UpdateBook(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldDeleteBook()
        {
            // Arrange
            var existingBookId = "1";
            _bookRepositoryMock.Setup(repo => repo.DeleteBookById(existingBookId))
                             .Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteBookByIdAsync(existingBookId);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.DeleteBookById(existingBookId), Times.Once);
        }

        [Fact]
        public async Task GetBooksByAuthorAsync_ShouldReturnBooksByAuthor()
        {
            // Arrange
            var author = "Example Author";
            var expectedBooks = new List<Book> { new Book { Title = "Book 1", Author = author }, new Book { Title = "Book 2", Author = author } };
            _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthor(author)).ReturnsAsync(expectedBooks);

            // Act
            var result = await _bookService.GetBooksByAuthorAsync(author);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBooks.Count, result.Count());

        }

        [Fact]
        public async Task GetBooksByGenreAsync_ShouldReturnBooksByGenre()
        {
            // Arrange
            var genreName = "Fantasy";
            var genre = new Genre { Name = genreName };
            var expectedBooks = new List<Book> { new Book { Title = "Book 1", Genre = genre }, new Book { Title = "Book 2", Genre = genre } };

            _genreRepositoryMock.Setup(repo => repo.GetGenreByNameAsync(genreName)).ReturnsAsync(genre);
            _bookRepositoryMock.Setup(repo => repo.GetBooksByGenre(genre)).ReturnsAsync(expectedBooks);

            // Act
            var result = await _bookService.GetBooksByGenreAsync(genreName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBooks.Count, result.Count());
        }

        [Fact]
        public void Validate_ValidRequest_ShouldReturnValid()
        {
            // Arrange
            var request = new CreateBookRequest
            (
                Title: "Test Book",
                Author: "John Doe",
                GenreId: "1",
                Price: 9.99,
                Description: "Test Description"
            );

            // Act
            var result = _createValidator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_InvalidTitle_ShouldReturnInvalid()
        {
            // Arrange
            var request = new CreateBookRequest
            (
                Title: "",
                Author: "John Doe",
                GenreId: "1",
                Price: 9.99,
                Description: "Test Description"
            );

            // Act
            var result = _createValidator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "Title");
        }



    }
}