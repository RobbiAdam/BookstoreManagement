using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookstoreDBContext _context;

        public GenreRepository(BookstoreDBContext context)
        {
            _context = context;
        }
        public async Task AddGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();            
        }

        public async Task DeleteGenreByIdAsync(string id)
        {
            _context.Genres.Remove(_context.Genres.Find(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(string id)
        {
            return await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Genre> GetGenreByNameAsync(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();            
        }
    }
}
