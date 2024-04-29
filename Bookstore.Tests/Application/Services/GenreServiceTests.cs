using Bookstore.Application.Mappers;
using Bookstore.Application.Services;
using Bookstore.Contract.Requests.Genre;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Validations.Genres;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Bookstore.Tests.Application.Services;
public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _genreRepositoryMock;
    private readonly Mock<CreateGenreRequestValidator> _createValidationsMock;
    private readonly Mock<UpdateGenreRequestValidator> _updateValidationsMock;
    private readonly Mock<ILogger<GenreService>> _loggerMock;
    private readonly GenreService _genreService;

    public GenreServiceTests()
    {
        _genreRepositoryMock = new Mock<IGenreRepository>();
        _createValidationsMock = new Mock<CreateGenreRequestValidator>();
        _updateValidationsMock = new Mock<UpdateGenreRequestValidator>();
        _loggerMock = new Mock<ILogger<GenreService>>();
        _genreService = new GenreService(_genreRepositoryMock.Object, _createValidationsMock.Object, _updateValidationsMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task AddGenreAsync_ShouldAddGenre()
    {
        // Arrange
        var request = new CreateGenreRequest (Name: "Genre"); 

        _genreRepositoryMock.Setup(r => r.AddGenreAsync(It.IsAny<Genre>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _genreService.AddGenreAsync(request);

        // Assert        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteGenreByIdAsync_ExistingGenre_ShouldDeleteGenre()
    {
        // Arrange
        var genreId = "genre-id";
        var genre = new Genre { Id = genreId };
        _genreRepositoryMock.Setup(r => r.GetGenreByIdAsync(genreId)).ReturnsAsync(genre);

        // Act
        await _genreService.DeleteGenreByIdAsync(genreId);

        // Assert
        _genreRepositoryMock.Verify(r => r.DeleteGenreByIdAsync(genreId), Times.Once);
    }

    [Fact]
    public async Task DeleteGenreByIdAsync_NonExistingGenre_ShouldThrowException()
    {
        // Arrange
        var genreId = "";
        _genreRepositoryMock.Setup(r => r.GetGenreByIdAsync(genreId)).ReturnsAsync((Genre)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _genreService.DeleteGenreByIdAsync(genreId));
    }

    [Fact]
    public async Task GetAllGenresAsync_ShouldReturnAllGenres()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = "genre-1", Name = "Genre 1" },
            new Genre { Id = "genre-2", Name = "Genre 2" }
        };
        _genreRepositoryMock.Setup(r => r.GetAllGenresAsync()).ReturnsAsync(genres);

        // Act
        var result = await _genreService.GetAllGenresAsync();

        // Assert
        Assert.Equal(genres.Count, result.Count());
        Assert.True(result.SequenceEqual(genres.Select(g => g.ToResponse())));
    }

    [Fact]
    public async Task GetGenreByIdAsync_ExistingGenre_ShouldReturnGenre()
    {
        // Arrange
        var genreId = "genre-id";
        var genre = new Genre { Id = genreId, Name = "Test Genre" };
        _genreRepositoryMock.Setup(r => r.GetGenreByIdAsync(genreId)).ReturnsAsync(genre);

        // Act
        var result = await _genreService.GetGenreByIdAsync(genreId);

        // Assert
        Assert.Equal(genre.ToResponse(), result);
    }

}