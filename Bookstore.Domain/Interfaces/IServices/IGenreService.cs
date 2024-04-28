using Bookstore.Contract.Requests.Genre;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Entities;
namespace Bookstore.Domain.Interfaces.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponse>> GetAllGenresAsync();
        Task<GenreResponse> GetGenreByIdAsync(string id);
        Task<GenreResponse> AddGenreAsync(CreateGenreRequest request);
        Task<GenreResponse> UpdateGenreAsync(string id, UpdateGenreRequest request);
        Task DeleteGenreByIdAsync(string id);
    }
}
