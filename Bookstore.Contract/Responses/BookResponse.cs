namespace Bookstore.Contract.Responses
{
    public record BookResponse
        (string Id, string Title, string Author, 
        string Description,string GenreName, double Price
        );
    
}
