using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Interfaces.IRepositories
{
    public interface IInventoryRepository
    {
        Task AddToInventory(Inventory inventory);
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory> GetInventoryById(string id);
        Task<Inventory> GetInventoryByBookId(string bookId);
        Task UpdateInventory(Inventory inventory);
        Task RemoveFromInventoryById(string id);

    }
}
