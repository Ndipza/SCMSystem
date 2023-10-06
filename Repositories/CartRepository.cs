using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SCMSystemDBContext _context;
        public CartRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteCart(int id, string? userId)
        {

            if (_context != null)
            {
                var cart = GetCartById(id)?.Result;
                if (cart != null && cart.CustomerId == new Guid(userId))
                {
                    _context.Carts.RemoveRange(cart);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            return false;
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await _context.Carts
                .Include(cart => cart.CartStatus).ToListAsync();
        }

        public async Task<Cart?> GetCartById(int id)
        {
            return await _context.Carts
                .Include(cart => cart.CartStatus)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> CreateCart(CartViewModel cartViewModel)
        {
            var cart = new Cart
            {
                DateCreated = DateTime.Now,
                CustomerId = cartViewModel.CustomerId,
                CartStatusId = cartViewModel.CartStatusId
            };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart.Id;
        }

        public async Task<Cart> UpdateCart(CartViewModel cartViewModel, int id)
        {
            var cart = GetCartById(id)?.Result;

            if (cart == null || cart.CustomerId != cartViewModel.CustomerId) { return new Cart(); }

            cart.Id = id;
            cart.DateUpdated = DateTime.Now;
            cart.CustomerId = cartViewModel.CustomerId;
            cart.CartStatusId = cartViewModel.CartStatusId;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
