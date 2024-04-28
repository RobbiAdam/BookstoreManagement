using Bookstore.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(string bookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.Identity.Name;
            await _cartService.AddToCart(username, bookId);
            return Ok();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(string bookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.Identity.Name;
            await _cartService.RemoveFromCart(username, bookId);
            return Ok();
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateCartItemQuantity(string bookId, int quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.Identity.Name;
            await _cartService.UpdateCartItemQuantity(username, bookId, quantity);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.Identity.Name;
            var cart = await _cartService.GetCart(username);
            return Ok(cart);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.Identity.Name;
            await _cartService.ClearCart(username);
            return Ok();
        }
    }
}