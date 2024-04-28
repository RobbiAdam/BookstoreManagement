using Bookstore.Contract.Requests.Inventory;
using Bookstore.Contract.Responses;
using Bookstore.Domain.Entities;
using Mapster;

namespace Bookstore.Application.Mappers
{
    public static class InventoryMapper
    {
        public static InventoryResponse ToResponse(this Inventory inventory)
        {
            var response = inventory.Adapt<InventoryResponse>();
            return response;
        }
        public static Inventory ToEntity(this CreateInventoryRequest request, Book book)
        {
            var newinventory = request.Adapt<Inventory>();
            return newinventory;
        }

        public static Inventory ToEntity(this UpdateInventoryRequest request, Inventory existingInventory)
        {
            var newinventory = request.Adapt(existingInventory);
            return newinventory;
        }
    }
}
