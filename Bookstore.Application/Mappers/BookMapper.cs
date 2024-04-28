using Bookstore.Contract.Requests.Book;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Entities;
using Mapster;

namespace Bookstore.Application.Mappers
{
    public static class BookMapper
    {
        public static BookResponse ToResponse(this Book book)
        {
            var response = book.Adapt<BookResponse>();
            return response;
        }
        public static Book ToEntity(this CreateBookRequest request, Genre genre)
        {
            var book = request.Adapt<Book>();
            return book;
        }
        public static Book ToEntity(this UpdateBookRequest request, Book existingBook)
        {
            var book = request.Adapt(existingBook);
            return book;
        }
    }
}
