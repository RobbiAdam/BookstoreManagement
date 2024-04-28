using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Interfaces.IServices;

namespace Bookstore.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;

        public CartService(ICartRepository cartRepository, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
        }

        public async Task AddToCart(string userId, string bookId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId) ?? new Cart { UserId = userId };
            var book = await _bookRepository.GetBookById(bookId);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            var existingItem = cart.CartItems.FirstOrDefault(i => i.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.CartItems.Add(new CartItem { BookId = bookId, Book = book, Quantity = 1 });
            }

            await _cartRepository.AddToCart(cart);
        }

        public async Task RemoveFromCart(string userId, string bookId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                return;
            }

            var item = cart.CartItems.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                cart.CartItems.Remove(item);
                await _cartRepository.UpdateCart(cart);
            }
        }

        public async Task UpdateCartItemQuantity(string userId, string bookId, int quantity)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                return;
            }

            var item = cart.CartItems.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                item.Quantity = quantity;
                await _cartRepository.UpdateCart(cart);
            }
        }

        public async Task<Cart> GetCart(string userId)
        {
            return await _cartRepository.GetCartByUserId(userId);
        }

        public async Task ClearCart(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                await _cartRepository.UpdateCart(cart);
            }
        }
    }
}