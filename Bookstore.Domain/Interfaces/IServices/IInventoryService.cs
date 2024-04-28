using Bookstore.Contract.Requests.Inventory;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Interfaces.IServices
{
    public interface IInventoryService
    {
        Task AddToInventory(CreateInventoryRequest request);
        Task<IEnumerable<InventoryResponse>> GetInventories();
        Task<InventoryResponse> GetInventoryById(string id);
        Task<InventoryResponse> UpdateInventoryQuantity(string id, UpdateInventoryRequest request);
        Task RemoveFromInventory(string id);
    }
}
