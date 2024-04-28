using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Interfaces.IRepositories
{
    public interface IGenreRepository 
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(string id);
        Task<Genre> GetGenreByNameAsync(string name);
        Task AddGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreByIdAsync(string id);

    }
}
