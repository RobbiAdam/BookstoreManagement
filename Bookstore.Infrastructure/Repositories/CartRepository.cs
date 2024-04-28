using Bookstore.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BookstoreDBContext _context;

        public CartRepository(BookstoreDBContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Book)
                .SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddToCart(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}