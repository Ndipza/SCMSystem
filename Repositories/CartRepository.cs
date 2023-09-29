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
        public async Task DeleteCart(int id)
        {
            var cart = GetCartById(id)?.Result ?? new Cart();
            _context.Carts.RemoveRange(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await _context.Carts
                .AsNoTracking()
                .Include(cart => cart.CartStatus)
                .Include(cart => cart.Customer).ToListAsync();
        }

        public async Task<Cart?> GetCartById(int id)
        {
            return await _context.Carts
                .AsNoTracking()
                .Include(cart => cart.CartStatus)
                .Include(cart => cart.Customer)
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

            if (cart == null) { return new Cart(); }

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
