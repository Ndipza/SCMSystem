using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly SCMSystemDBContext _context;
        public PaymentMethodRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var paymentMethod = GetById(id)?.Result ?? new PaymentMethod();
            _context.PaymentMethods.RemoveRange(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PaymentMethod>> GetAll()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod?> GetById(int id)
        {
            return await _context.PaymentMethods
                .Include(payment => payment.Payments)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(PaymentMethodViewModel paymentMethodViewModel)
        {
            var paymentMethod = new PaymentMethod
            {
                Description = paymentMethodViewModel.Name
            };
            await _context.PaymentMethods.AddAsync(paymentMethod).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return paymentMethod.Id;
        }

        public async Task<PaymentMethod> UpdateAsync(PaymentMethodViewModel paymentMethodViewModel, int id)
        {
            var paymentMethod = GetById(id)?.Result;

            if (paymentMethod == null) { return new PaymentMethod(); }
            
            paymentMethod.Description = paymentMethodViewModel.Name;
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }
    }
}
