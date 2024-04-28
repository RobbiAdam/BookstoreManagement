namespace Bookstore.Contract.Requests.Inventory
{
    public record CreateInventoryRequest
    (string BookId, int Quantity);
}
