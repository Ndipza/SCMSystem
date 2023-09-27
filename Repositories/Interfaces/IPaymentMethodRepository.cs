using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethod>> GetAll();
        Task<PaymentMethod?> GetById(int id);
        Task<long> InsertAsync(PaymentMethodViewModel paymentMethodViewModel);
        Task<PaymentMethod> UpdateAsync(PaymentMethodViewModel paymentMethodViewModel, int id);
        Task Delete(int id);
    }
}
