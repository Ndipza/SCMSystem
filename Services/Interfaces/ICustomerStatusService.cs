using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface ICustomerStatusService
    {
        Task<List<CustomerStatus>> GetAllCustomerStatuses();
        Task<CustomerStatus?> GetCustomerStatusById(int id);
        Task<long> CreateCustomerStatus(CustomerStatusViewModel customerStatusViewModel);
        Task<CustomerStatus> UpdateCustomerStatus(CustomerStatusViewModel customerStatusViewModel, int id);
        Task<bool> DeleteCustomerStatusById(int id);
    }
}
