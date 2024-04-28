using Bookstore.Contract.Requests.ShoppingCart;
using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingCart()
        {
            var userId = User.Identity.Name;
            var cart = await _shoppingCartService.GetShoppingCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.Identity.Name;
            var cart = await _shoppingCartService.AddToCartAsync(userId, request);
            return Ok(cart);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(string itemId, [FromBody] int newQuantity)
        {
            var userId = User.Identity.Name;
            var cart = await _shoppingCartService.UpdateCartItemQuantityAsync(userId, itemId, newQuantity);
            return Ok(cart);
        }

        [HttpDelete("{item}")]
        public IActionResult RemoveCartItem(string item)
        {
            var userId = User.Identity.Name;
            var cart = _shoppingCartService.RemoveFromCartAsync(userId, item);

            return Ok(cart);
        }
    }
}