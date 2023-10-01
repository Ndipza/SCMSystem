using Core.ViewModels;
using Data.Models;
using Data;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        private readonly SCMSystemDBContext _context;
        public PaymentStatusRepository(SCMSystemDBContext context)
        {
            _context = context;
        }

        public async Task<long> CreatePaymentStatusAsync(PaymentStatusViewModel PaymentStatusViewModel)
        {
            var paymentStatus = new PaymentStatus
            {
                Description = PaymentStatusViewModel.Description
            };
            await _context.PaymentStatuses.AddAsync(paymentStatus).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return paymentStatus.Id;
        }

        public async Task<bool> DeletePaymentStatusById(int id)
        {
            if (_context != null)
            {
                var paymentStatus = GetPaymentStatusById(id)?.Result;
                if (paymentStatus != null)
                {
                    _context.PaymentStatuses.RemoveRange(paymentStatus);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            return false;
        }

        public async Task<List<PaymentStatus>> GetAllPaymentStatuses()
        {
            return await _context.PaymentStatuses.ToListAsync();
        }

        public async Task<PaymentStatus?> GetPaymentStatusById(int id)
        {
            return await _context.PaymentStatuses
                .Include(payment => payment.Payments)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaymentStatus> UpdatePaymentStatusAsync(PaymentStatusViewModel PaymentStatusViewModel, int id)
        {
            var paymentStatus = GetPaymentStatusById(id)?.Result;

            if (paymentStatus == null) { return new PaymentStatus(); }

            paymentStatus.Description = PaymentStatusViewModel.Description;
            _context.PaymentStatuses.Update(paymentStatus);
            await _context.SaveChangesAsync();
            return paymentStatus;
        }
    }
}
