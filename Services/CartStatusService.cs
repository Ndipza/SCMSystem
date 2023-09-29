using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class CartStatusService : ICartStatusService
    {
        private readonly SCMSystemDBContext _context;
        public CartStatusService(SCMSystemDBContext context)
        {
            _context = context;
        }

        public async Task<long> CreateCartStatusAsync(CartStatusViewModel cartStatusViewModel)
        {
            var CartStatus = new CartStatus
            {
                Description = cartStatusViewModel.Description
            };
            await _context.CartStatuses.AddAsync(CartStatus).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return CartStatus.Id;
        }

        public async Task DeleteCartStatusById(int id)
        {
            var CartStatus = GetCartStatusById(id)?.Result ?? new CartStatus();
            _context.CartStatuses.RemoveRange(CartStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartStatus>> GetAllCartStatuses()
        {
            return await _context.CartStatuses.ToListAsync();
        }

        public async Task<CartStatus?> GetCartStatusById(int id)
        {
            return await _context.CartStatuses
                .AsNoTracking()
                .Include(payment => payment.Carts)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CartStatus> UpdateCartStatusAsync(CartStatusViewModel cartStatusViewModel, int id)
        {
            var CartStatus = GetCartStatusById(id)?.Result;

            if (CartStatus == null) { return new CartStatus(); }

            CartStatus = new CartStatus
            {
                Id = id,
                Description = cartStatusViewModel.Description
            };
            _context.CartStatuses.Update(CartStatus);
            await _context.SaveChangesAsync();
            return CartStatus;
        }
    }
}
