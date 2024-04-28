namespace Bookstore.Domain.Interfaces.IRepositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(string userId);
        Task AddToCart(Cart cart);
        Task UpdateCart(Cart cart);
    }
}