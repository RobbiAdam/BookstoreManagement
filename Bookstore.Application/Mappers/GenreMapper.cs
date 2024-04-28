using Bookstore.Contract.Requests.Genre;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Entities;
using Mapster;

namespace Bookstore.Application.Mappers
{
    public static class GenreMapper
    {
        public static GenreResponse ToResponse(this Genre genre)
        {
            var response = genre.Adapt<GenreResponse>();            
            return response;
        }
        public static Genre ToEntity(this CreateGenreRequest request)
        {
            var genre = request.Adapt<Genre>();
            return genre;
        }
        public static Genre ToEntity(this UpdateGenreRequest request, Genre existingGenre)
        {
            var genre = request.Adapt(existingGenre);
            return genre;
        }
    }
}
