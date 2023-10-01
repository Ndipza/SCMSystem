using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayments();
        Task<Payment?> GetPaymentById(int id);
        Task<long> CreatePayment(PaymentViewModel paymentViewModel);
        Task<Payment> UpdatePayment(PaymentViewModel paymentViewModel, int id);
        Task<bool> DeletePayment(int id);
    }
}
