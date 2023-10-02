using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(Guid id);
        Task<Guid> CreateCustomer(CustomerViewModel customerViewModel);
        Task<Customer> UpdateCustomer(CustomerViewModel customerViewModel, Guid id);
        Task<bool> DeleteCustomer(Guid id);
    }
}
