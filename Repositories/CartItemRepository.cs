using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly SCMSystemDBContext _context;
        public CartItemRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task<long> CreateCartItem(CartItemViewModel cartItemViewModel)
        {
            var cartItem = new CartItem
            {
                CartId = cartItemViewModel.CartId,
                ProductId = cartItemViewModel.ProductId,
                Quantity = cartItemViewModel.Quantity
            };
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem.Id;
        }

        public async Task DeleteCartItem(int id)
        {
            var CartItem = GetCartItemById(id)?.Result ?? new CartItem();
            _context.CartItems.RemoveRange(CartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            return await _context.CartItems
                .AsNoTracking()
                .Include(p => p.Cart)
                .Include(p => p.Product).ToListAsync();
        }

        public async Task<CartItem?> GetCartItemById(int id)
        {
            return await _context.CartItems
                .AsNoTracking()
                .Include(CartItem => CartItem.Cart)
                .Include(CartItem => CartItem.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CartItem> UpdateCartItem(CartItemViewModel cartItemViewModel, int id)
        {
            var CartItem = GetCartItemById(id)?.Result;

            if (CartItem == null) { return new CartItem(); }

            CartItem.Id = id;
            CartItem.CartId = cartItemViewModel.CartId;
            CartItem.ProductId = cartItemViewModel.ProductId;
            CartItem.Quantity = cartItemViewModel.Quantity;

            _context.CartItems.Update(CartItem);
            await _context.SaveChangesAsync();
            return CartItem;
        }
    }
}
