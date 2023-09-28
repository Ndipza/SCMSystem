using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethod>> GetAllPaymentMethod();
        Task<PaymentMethod?> GetPaymentMethodById(int id);
        Task<long> CreatePaymentMethodAsync(PaymentMethodViewModel paymentMethodViewModel);
        Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethodViewModel paymentMethodViewModel, int id);
        Task Delete(int id);
    }
}
