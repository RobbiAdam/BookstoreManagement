using Bookstore.Application.Mappers;
using Bookstore.Contract.Requests.Genre;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Interfaces.IServices;
using Bookstore.Domain.Validations.Genres;
using Microsoft.Extensions.Logging;

namespace Bookstore.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly CreateGenreRequestValidator _createValidations;
        private readonly UpdateGenreRequestValidator _updateValidations;
        private readonly ILogger<GenreService> _logger;

        public GenreService(IGenreRepository genreRepository,
            CreateGenreRequestValidator createValidations,
            UpdateGenreRequestValidator updateValidations,
            ILogger<GenreService> logger)
        {
            _genreRepository = genreRepository;
            _createValidations = createValidations;
            _updateValidations = updateValidations;
            _logger = logger;
        }
        public async Task<GenreResponse> AddGenreAsync(CreateGenreRequest request)
        {
            var validationResult = _createValidations.Validate(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for genre with name: {GenreName}", request.Name);
                throw new Exception(validationResult.Errors[0].ErrorMessage);
            }

            _logger.LogInformation("Adding genre with name: {GenreName}", request.Name);
            var genre = request.ToEntity();
            await _genreRepository.AddGenreAsync(genre);
            return genre.ToResponse();
        }

        public async Task DeleteGenreByIdAsync(string id)
        {
            _logger.LogInformation("Deleting genre with id: {GenreId}", id);
            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null)
            {
                _logger.LogWarning("Genre with id: {GenreId} not found", id);
                throw new Exception("Genre not found");
            }
            await _genreRepository.DeleteGenreByIdAsync(id);
        }

        public async Task<IEnumerable<GenreResponse>> GetAllGenresAsync()
        {
            _logger.LogInformation("Getting all genres");
            var genres = await _genreRepository.GetAllGenresAsync();
            return genres.Select(x => x.ToResponse());
        }

        public async Task<GenreResponse> GetGenreByIdAsync(string id)
        {
            _logger.LogInformation("Getting genre with id: {GenreId}", id);
            var genre = await _genreRepository.GetGenreByIdAsync(id);
            return genre.ToResponse();
        }

        public async Task<GenreResponse> UpdateGenreAsync(string id, UpdateGenreRequest request)
        {
            var validationResult = _updateValidations.Validate(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for genre with id: {GenreId}", id);
                throw new Exception(validationResult.Errors[0].ErrorMessage);
            }

            _logger.LogInformation("Updating genre with id: {GenreId}", id);
            var existingGenre = await _genreRepository.GetGenreByIdAsync(id);
            if (existingGenre == null)
            {
                _logger.LogWarning("Genre with id: {GenreId} not found", id);
                throw new Exception("Genre not found");
            }

            var updatedGenre = request.ToEntity(existingGenre);
            await _genreRepository.UpdateGenreAsync(updatedGenre);
            return updatedGenre.ToResponse();
        }
    }
}
