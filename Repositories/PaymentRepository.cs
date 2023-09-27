using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SCMSystemDBContext _context;
        public PaymentRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var category = GetById(id)?.Result ?? new Payment();
            _context.Payments.RemoveRange(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment?> GetById(int id)
        {
            return await _context.Payments
                .AsNoTracking()
                .Include(payment => payment.PaymentMethod)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(PaymentViewModel paymentViewModel)
        {
            var payment = new Payment
            {
                PaymentMethodID = paymentViewModel.PaymentMethodID,
                CustomerId = paymentViewModel.CustomerId,
                OrderItemId = paymentViewModel.OrderItemId,
                PaymentDate = DateTime.Now,
                Amount = paymentViewModel.Amount
            };
            await _context.Payments.AddAsync(payment);
            return payment.Id;
        }

        public async Task<Payment> UpdateAsync(PaymentViewModel paymentViewModel, int id)
        {
            var payment = GetById(id)?.Result;

            if (payment == null) { return new Payment(); }

            payment = new Payment
            {
                Id = id,
                PaymentMethodID = paymentViewModel.PaymentMethodID,
                CustomerId = paymentViewModel.CustomerId,
                OrderItemId = paymentViewModel.OrderItemId,
                PaymentDate = DateTime.Now,
                Amount = paymentViewModel.Amount
            };
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
