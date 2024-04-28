namespace Bookstore.Contract.Requests.Book
{
    public record UpdateBookRequest
        (string Title, string Description, string Author, string GenreId, double Price);

}
