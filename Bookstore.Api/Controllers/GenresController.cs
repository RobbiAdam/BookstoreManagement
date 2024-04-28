using Bookstore.Contract.Requests.Genre;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{genreId}")]
        public async Task<IActionResult> GetGenre(string genreId)
        {
            var genre = await _genreService.GetGenreByIdAsync(genreId);
            return Ok(genre);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdGenre = await _genreService.AddGenreAsync(request);
            return Ok(createdGenre);
        }

        [HttpPut("{genreId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGenre(string genreId, [FromBody] UpdateGenreRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedGenre = await _genreService.UpdateGenreAsync(genreId, request);
            return Ok(updatedGenre);
        }

        [HttpDelete("{genreId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGenre(string genreId)
        {
            await _genreService.DeleteGenreByIdAsync(genreId);
            return Ok();
        }

    }
}
