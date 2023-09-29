using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface IPaymentStatusService
    {
        Task<List<PaymentStatus>> GetAllPaymentStatuses();
        Task<PaymentStatus?> GetPaymentStatusById(int id);
        Task<long> CreatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel);
        Task<PaymentStatus> UpdatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel, int id);
        Task DeletePaymentStatusById(int id);
    }
}
