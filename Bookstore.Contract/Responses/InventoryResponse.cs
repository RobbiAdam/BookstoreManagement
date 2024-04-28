namespace Bookstore.Contract.Responses
{
    public record InventoryResponse
    (string Id, string BookTitle, string BookAuthor, string BookDescription, string BookGenreName, int Quantity);
}
