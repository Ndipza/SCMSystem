using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetAll();
        Task<Payment?> GetById(int id);
        Task<long> InsertAsync(PaymentViewModel paymentViewModel);
        Task<Payment> UpdateAsync(PaymentViewModel paymentViewModel, int id);
        Task Delete(int id);
    }
}
