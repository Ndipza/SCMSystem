using Core.ViewModels;
using Data.Models;
using Data;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly SCMSystemDBContext _context;
        public PaymentStatusService(SCMSystemDBContext context)
        {
            _context = context;
        }

        public async Task<long> CreatePaymentStatusAsync(PaymentStatusViewModel PaymentStatusViewModel)
        {
            var PaymentStatus = new PaymentStatus
            {
                Description = PaymentStatusViewModel.Description
            };
            await _context.PaymentStatuses.AddAsync(PaymentStatus).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return PaymentStatus.Id;
        }

        public async Task DeletePaymentStatusById(int id)
        {
            var PaymentStatus = GetPaymentStatusById(id)?.Result ?? new PaymentStatus();
            _context.PaymentStatuses.RemoveRange(PaymentStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PaymentStatus>> GetAllPaymentStatuses()
        {
            return await _context.PaymentStatuses.ToListAsync();
        }

        public async Task<PaymentStatus?> GetPaymentStatusById(int id)
        {
            return await _context.PaymentStatuses
                .AsNoTracking()
                .Include(payment => payment.Payments)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaymentStatus> UpdatePaymentStatusAsync(PaymentStatusViewModel PaymentStatusViewModel, int id)
        {
            var PaymentStatus = GetPaymentStatusById(id)?.Result;

            if (PaymentStatus == null) { return new PaymentStatus(); }

            PaymentStatus = new PaymentStatus
            {
                Id = id,
                Description = PaymentStatusViewModel.Description
            };
            _context.PaymentStatuses.Update(PaymentStatus);
            await _context.SaveChangesAsync();
            return PaymentStatus;
        }
    }
}
