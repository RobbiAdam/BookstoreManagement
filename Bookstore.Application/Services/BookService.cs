using Bookstore.Application.Mappers;
using Bookstore.Contract.Requests.Book;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Interfaces.IServices;

namespace Bookstore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookResponse> AddBookAsync(CreateBookRequest request)
        {
            var existingGenre = await _genreRepository.GetGenreByIdAsync(request.GenreId);
            if (existingGenre == null)
            {
                throw new Exception("Genre not found");
            }

            var newBook = request.ToEntity(existingGenre);
            await _bookRepository.AddBook(newBook);
            return newBook.ToResponse();
        }
        public async Task<IEnumerable<BookResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooks();
            return books.Select(x => x.ToResponse());
        }

        public async Task<BookResponse> GetBookByIdAsync(string id)
        {
            var book = await _bookRepository.GetBookById(id);
            return book.ToResponse();
        }

        public async Task<BookResponse> UpdateBookAsync(string Id, UpdateBookRequest request)
        {
            var existingBook = await _bookRepository.GetBookById(Id);
            if (existingBook == null)
            {
                throw new Exception("Book not found");
            }
            var existingGenre = await _genreRepository.GetGenreByIdAsync(request.GenreId);
            var updatedBook = request.ToEntity(existingBook);
            updatedBook.Genre = existingGenre;

            await _bookRepository.UpdateBook(updatedBook);
            return updatedBook.ToResponse();
        }
        public async Task DeleteBookByIdAsync(string id)
        {
            await _bookRepository.DeleteBookById(id);
        }

        public async Task<IEnumerable<BookResponse>> GetBooksByAuthorAsync(string author)
        {
            var books = await _bookRepository.GetBooksByAuthor(author);
            return books.Select(x => x.ToResponse());
        }

        public async Task<IEnumerable<BookResponse>> GetBooksByGenreAsync(string genreName)
        {
            var genre = await _genreRepository.GetGenreByNameAsync(genreName);
            if (genre == null)
            {
                throw new Exception("Genre not found");
            }

            var books = await _bookRepository.GetBooksByGenre(genre);
            return books.Select(x => x.ToResponse());
        }
    }
}
