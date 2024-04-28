using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly BookstoreDBContext _context;

        public InventoryRepository(BookstoreDBContext context)
        {
            _context = context;
        }

        public async Task AddToInventory(Inventory inventory)
        {
            await _context.AddAsync(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetInventoryById(string id)
        {
            return await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Inventory> GetInventoryByBookId(string bookId)
        {
            return await _context.Inventories.FirstOrDefaultAsync(x => x.BookId == bookId);
        }

        public async Task UpdateInventory(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromInventoryById(string id)
        {
            _context.Inventories.Remove(_context.Inventories.Find(id));
            await _context.SaveChangesAsync();
        }
    }
}
