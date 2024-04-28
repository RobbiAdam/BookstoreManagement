using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly BookstoreDBContext _context;

        public ShoppingCartRepository(BookstoreDBContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCart> GetShoppingCartAsync(string userId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task UpdateShoppingCartAsync(ShoppingCart cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
