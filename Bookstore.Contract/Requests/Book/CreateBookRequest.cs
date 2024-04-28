namespace Bookstore.Contract.Requests.Book
{
    public record CreateBookRequest
    (string Title,string GenreId, string Description, string Author, double Price);
}
