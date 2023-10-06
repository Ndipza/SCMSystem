using Core.ViewModels;
using Data;
using Data.Models;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CartStatusRepository : ICartStatusRepository
    {
        private readonly SCMSystemDBContext _context;
        public CartStatusRepository(SCMSystemDBContext context)
        {
            _context = context;
        }

        public async Task<long> CreateCartStatusAsync(CartStatusViewModel cartStatusViewModel)
        {
            var cartStatus = new CartStatus
            {
                Description = cartStatusViewModel.Description
            };
            await _context.CartStatuses.AddAsync(cartStatus).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return cartStatus.Id;
        }

        public async Task<bool> DeleteCartStatusById(int id)
        {
            if (_context != null)
            {
                var cartStatus = GetCartStatusById(id)?.Result;
                if (cartStatus != null)
                {
                    _context.CartStatuses.RemoveRange(cartStatus);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        public async Task<List<CartStatus>> GetAllCartStatuses()
        {
            return await _context.CartStatuses.ToListAsync();
        }

        public async Task<CartStatus?> GetCartStatusById(int id)
        {
            return await _context.CartStatuses
                .Include(payment => payment.Carts)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CartStatus> UpdateCartStatusAsync(CartStatusViewModel cartStatusViewModel, int id)
        {
            var cartStatus = GetCartStatusById(id)?.Result;

            if (cartStatus == null) { return new CartStatus(); }

            cartStatus.Description = cartStatusViewModel.Description;
            _context.CartStatuses.Update(cartStatus);
            await _context.SaveChangesAsync();
            return cartStatus;
        }
    }
}
