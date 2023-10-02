using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(Guid id);
        Task<Guid> CreateCustomer(CustomerViewModel customerViewModel);
        Task<Customer> UpdateCustomers(CustomerViewModel customerViewModel, Guid id);
        Task<bool> DeleteCustomer(Guid id);
    }
}
