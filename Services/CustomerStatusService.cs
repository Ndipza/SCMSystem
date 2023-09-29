using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class CustomerStatusService : ICustomerStatusService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CustomerStatusService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<long> CreateCustomerStatusAsync(CustomerStatusViewModel customerStatusViewModel)
        {
            return await _unitOfWork.CustomerStatusRepository.CreateCustomerStatusAsync(customerStatusViewModel);
        }

        public async Task DeleteCustomerStatusById(int id)
        {
            await _unitOfWork.CustomerStatusRepository.DeleteCustomerStatusById(id); 
        }

        public async Task<List<CustomerStatus>> GetAllCustomerStatuses()
        {
            return await _unitOfWork.CustomerStatusRepository.GetAllCustomerStatuses();
        }

        public async Task<CustomerStatus?> GetCustomerStatusById(int id)
        {
            return await _unitOfWork.CustomerStatusRepository.GetCustomerStatusById(id);
        }

        public async Task<CustomerStatus> UpdateCustomerStatusAsync(CustomerStatusViewModel customerStatusViewModel, int id)
        {
            return await _unitOfWork.CustomerStatusRepository.UpdateCustomerStatusAsync(customerStatusViewModel, id);
        }
    }
}
