using Bookstore.Contract.Requests.Book;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBook(string bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            return Ok(book);
        }

        [HttpPost]
        [Authorize (Roles = "admin")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
        {
            var createdBook = await _bookService.AddBookAsync(request);
            return Ok(createdBook);
        }

        [HttpPut("{bookId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook(string bookId, [FromBody] UpdateBookRequest request)
        {
            var updatedBook = await _bookService.UpdateBookAsync(bookId, request);
            return Ok(updatedBook);
        }

        [HttpDelete("{bookId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            await _bookService.DeleteBookByIdAsync(bookId);
            return NoContent();
        }

        [HttpGet("by-author/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {
            var result = await _bookService.GetBooksByAuthorAsync(author);
            return Ok(result);
        }

        [HttpGet("by-genre/{genreName}")]
        public async Task<IActionResult> GetBooksByGenre(string genreName)
        {
            var result = await _bookService.GetBooksByGenreAsync(genreName);
            return Ok(result);
        }
    }
}
