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
                .Include(cart => cart.CartStatus)
                .Include(cart => cart.Customer)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(CartViewModel cartViewModel)
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

        public async Task<Cart> UpdateAsync(CartViewModel cartViewModel, int id)
        {
            var cart = GetById(id)?.Result;

            if (cart == null) { return new Cart(); }

            cart = new Cart
            {
                Id = id,
                DateUpdated = DateTime.Now,
                CustomerId = cartViewModel.CustomerId,
                CartStatusId = cartViewModel.CartStatusId
            };
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
