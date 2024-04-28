using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Interfaces.IRepositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(string id);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBookById(string id);
        Task<IEnumerable<Book>> GetBooksByAuthor(string author);
        Task<IEnumerable<Book>> GetBooksByGenre(Genre genre);
    }
}
