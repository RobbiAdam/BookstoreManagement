using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDBContext _context;

        public BookRepository(BookstoreDBContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookById(string id)
        {
            _context.Books.Remove(_context.Books.Find(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(string id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            return await _context.Books.Where(x => x.Author == author).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(Genre genre)
        {
            return await _context.Books.Where(x => x.Genre == genre).ToListAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
