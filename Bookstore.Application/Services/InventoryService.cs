using Bookstore.Application.Mappers;
using Bookstore.Contract.Requests.Inventory;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Interfaces.IServices;


namespace Bookstore.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IBookRepository _bookRepository;

        public InventoryService(IInventoryRepository inventoryRepository, IBookRepository bookRepository )
        {
            _inventoryRepository = inventoryRepository;
            _bookRepository = bookRepository;
        }

        public async Task AddToInventory(CreateInventoryRequest request)
        {
            var existingBook = await _bookRepository.GetBookById(request.BookId);

            if (existingBook == null)
            {
                throw new Exception("Book not found");
            }

            var existingInventory = await _inventoryRepository.GetInventoryByBookId(request.BookId);
            if (existingInventory != null)
            {
                throw new Exception("Book already exists in the inventory");
            }

            var inventory = request.ToEntity(existingBook);   
            await _inventoryRepository.AddToInventory(inventory);
        }

        public async Task<IEnumerable<InventoryResponse>> GetInventories()
        {          
            var inventories = await _inventoryRepository.GetAllInventories();
            return inventories.Select(x => x.ToResponse());
        }

        public async Task<InventoryResponse> GetInventoryById(string id)
        {
            var inventory = await _inventoryRepository.GetInventoryById(id);
            return inventory.ToResponse();
        }

        public async Task<InventoryResponse> UpdateInventoryQuantity(string Id, UpdateInventoryRequest request)
        {
            var existingInventory = await _inventoryRepository.GetInventoryById(Id);
            if (existingInventory == null)
            {
                throw new Exception("Inventory not found");
            }
            var updatedInventory = request.ToEntity(existingInventory);
            await _inventoryRepository.UpdateInventory(updatedInventory);
            return updatedInventory.ToResponse();           

        }
        public async Task RemoveFromInventory(string id)
        {
            await _inventoryRepository.RemoveFromInventoryById(id);
        }
    }
}
