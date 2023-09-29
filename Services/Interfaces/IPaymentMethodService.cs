using Core.ViewModels;
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
        Task<List<PaymentMethod>> GetAllPaymentMethods();
        Task<PaymentMethod?> GetPaymentMethodById(int id);
        Task<long> CreatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel);
        Task<PaymentMethod> UpdatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel, int id);
        Task DeletePaymentMethod(int id);
    }
}
