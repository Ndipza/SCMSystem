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
        public async Task<long?> CreateCartItem(CartItemViewModel cartItemViewModel, string? userId)
        {
            List<int>? cartIds = GetCartIdsOfLoginUser(userId);

            if (cartIds == null && !cartIds.Contains(cartItemViewModel.CartId)) return null;

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

        public async Task<bool> DeleteCartItem(int id, string? userId)
        {
            if (_context != null)
            {
                var cartItem = GetCartItemById(id, userId)?.Result;
                if (cartItem != null)
                {
                    _context.CartItems.RemoveRange(cartItem);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            return false;
        }

        public async Task<List<CartItem>> GetAllCartItems(string? userId)
        {
            List<int>? cartIds = GetCartIdsOfLoginUser(userId);

            if (cartIds == null) return null;

            return await _context.CartItems.Where(c => cartIds.Contains((int)c.CartId))             
                .Include(p => p.Cart)
                .Include(p => p.Product).ToListAsync();
        }

        public async Task<CartItem?> GetCartItemById(int id, string? userId)
        {
            List<int>? cartIds = GetCartIdsOfLoginUser(userId);

            if(cartIds == null) return null;

            return await _context.CartItems.Where(c => cartIds.Contains((int)c.CartId))
                .Include(CartItem => CartItem.Cart)
                .Include(CartItem => CartItem.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        private List<int>? GetCartIdsOfLoginUser(string? userId)
        {
            return _context.Carts?.Where(x => x.CustomerId == new Guid(userId))?.ToList().Select(c => c.Id).ToList();
        }

        public async Task<CartItem?> UpdateCartItem(CartItemViewModel cartItemViewModel, int id, string? userId)
        {
            var cartItem = GetCartItemById(id, userId)?.Result;

            if (cartItem == null) { return null; }

            cartItem.Id = id;
            cartItem.CartId = cartItemViewModel.CartId;
            cartItem.ProductId = cartItemViewModel.ProductId;
            cartItem.Quantity = cartItemViewModel.Quantity;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}
