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
        public async Task Delete(int id)
        {
            var cart = GetById(id)?.Result ?? new Cart();
            _context.Carts.RemoveRange(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart?> GetById(int id)
        {
            return await _context.Carts
                .AsNoTracking()
                .Include(cart => cart.Order)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(CartViewModel cartViewModel)
        {
            var cart = new Cart
            {
                Quantity = cartViewModel.Quantity,
                CustomerId = cartViewModel.CustomerId,
                OrderId = cartViewModel.OrderId
            };
            await _context.Carts.AddAsync(cart);
            return cart.Id;
        }

        public async Task<Cart> UpdateAsync(CartViewModel cartViewModel, int id)
        {
            var cart = GetById(id)?.Result;

            if (cart == null) { return new Cart(); }

            cart = new Cart
            {
                Id = id,
                Quantity = cartViewModel.Quantity,
                CustomerId = cartViewModel.CustomerId,
                OrderId = cartViewModel.OrderId
            };
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
