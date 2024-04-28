using Bookstore.Contract.Requests.Book;
using Bookstore.Contract.Responses;

namespace Bookstore.Domain.Interfaces.IServices
{
    public interface IBookService 
    {
        Task<IEnumerable<BookResponse>> GetAllBooksAsync();
        Task<BookResponse> GetBookByIdAsync(string id);
        Task<BookResponse> AddBookAsync(CreateBookRequest request);
        Task<BookResponse> UpdateBookAsync(string Id, UpdateBookRequest request);
        Task DeleteBookByIdAsync(string id);
        Task<IEnumerable<BookResponse>> GetBooksByAuthorAsync(string author);
        Task<IEnumerable<BookResponse>> GetBooksByGenreAsync(string genreName);
    }
}
