using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CustomerService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateCustomer(CustomerViewModel customerViewModel)
        {
            return await _unitOfWork.CustomerRepository.CreateCustomer(customerViewModel);
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            return await _unitOfWork.CustomerRepository.DeleteCustomer(id);
        }

        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _unitOfWork.CustomerRepository.GetAllCustomer();
        }

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await _unitOfWork.CustomerRepository.GetCustomerById(id);
        }

        public async Task<Customer> UpdateCustomer(CustomerViewModel customerViewModel, Guid id)
        {
            return await _unitOfWork.CustomerRepository.UpdateCustomer(customerViewModel, id);
        }
    }
}
