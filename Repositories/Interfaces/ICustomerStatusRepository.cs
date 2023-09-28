using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICustomerStatusRepository
    {
        Task<List<CustomerStatus>> GetAllCustomerStatuses();
        Task<CustomerStatus?> GetCustomerStatusById(int id);
        Task<long> CreateCustomerStatusAsync(CustomerStatusViewModel customerStatusViewModel);
        Task<CustomerStatus> UpdateCustomerStatusAsync(CustomerStatusViewModel customerStatusViewModel, int id);
        Task DeleteCustomerStatusById(int id);
    }
}
