using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IPaymentStatusRepository
    {
        Task<List<PaymentStatus>> GetAllPaymentStatuses();
        Task<PaymentStatus?> GetPaymentStatusById(int id);
        Task<long> CreatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel);
        Task<PaymentStatus> UpdatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel, int id);
        Task<bool> DeletePaymentStatusById(int id);
    }
}
