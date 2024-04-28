namespace Bookstore.Contract.Requests.ShoppingCart
{
    public record AddToCartRequest
        (string BookId, int Quantity);

}
