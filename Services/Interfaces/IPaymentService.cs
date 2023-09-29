using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetAllPayments();
        Task<Payment?> GetPaymentById(int id);
        Task<long> CreatePayment(PaymentViewModel paymentViewModel);
        Task<Payment> UpdatePayment(PaymentViewModel paymentViewModel, int id);
        Task DeletePayment(int id);
    }
}
