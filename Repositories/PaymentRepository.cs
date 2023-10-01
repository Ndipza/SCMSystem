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
        public async Task<bool> DeletePayment(int id)
        {
            if (_context != null)
            {
                var payment = GetPaymentById(id)?.Result;
                if (payment != null)
                {
                    _context.Payments.RemoveRange(payment);
                    await _context.SaveChangesAsync();

                    return true;
                }

            }
            return false;
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _context.Payments
                .Include(payment => payment.PaymentMethod)
                .Include(payment => payment.PaymentStatus)
                .Include(payment => payment.Cart).ToListAsync();
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            return await _context.Payments
                .Include(payment => payment.PaymentMethod)
                .Include(payment => payment.PaymentStatus)
                .Include(payment => payment.Cart)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> CreatePayment(PaymentViewModel paymentViewModel)
        {
            var payment = new Payment
            {
                PaymentMethodId = paymentViewModel.PaymentMethodID,
                CartId = paymentViewModel.CartId,
                PaymentStatusId = paymentViewModel.PaymentStatusId,
                PaymentDate = DateTime.Now,
                Balance = paymentViewModel.Amount
            };
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment.Id;
        }

        public async Task<Payment> UpdatePayment(PaymentViewModel paymentViewModel, int id)
        {
            var payment = GetPaymentById(id)?.Result;

            if (payment == null) { return new Payment(); }

            payment.Id = id;
            payment.PaymentMethodId = paymentViewModel.PaymentMethodID;
            payment.PaymentStatusId = paymentViewModel.PaymentStatusId;
            payment.CartId = paymentViewModel.CartId;
            payment.PaymentDate = DateTime.Now;
            payment.Balance = paymentViewModel.Amount;

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
