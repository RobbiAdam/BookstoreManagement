namespace Bookstore.Domain.Interfaces.IServices
{
    public interface ICartService
    {
        Task AddToCart(string userId, string bookId);
        Task RemoveFromCart(string userId, string bookId);
        Task UpdateCartItemQuantity(string userId, string bookId, int quantity);
        Task<Cart> GetCart(string userId);
        Task ClearCart(string userId);
    }
}