using Bookstore.Contract.Requests.Inventory;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToInventory([FromBody] CreateInventoryRequest request)
        {
            try
            {
                await _inventoryService.AddToInventory(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInventories()
        {
            var inventories = await _inventoryService.GetInventories();
            return Ok(inventories);
        }
        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetInventory(string inventoryId)
        {
            var inventory = await _inventoryService.GetInventoryById(inventoryId);
            return Ok(inventory);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInventoryQuantity(string inventoryId, [FromBody] UpdateInventoryRequest request)
        {
            await _inventoryService.UpdateInventoryQuantity(inventoryId, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromInventory(string inventoryId)
        {
            await _inventoryService.RemoveFromInventory(inventoryId);
            return Ok();
        }
    }
}
