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
        Task<List<Customer>> GetAll();
        Task<Customer?> GetById(Guid id);
        Task<Guid> InsertAsync(CustomerViewModel customerViewModel);
        Task<Customer> UpdateAsync(CustomerViewModel customerViewModel, Guid id);
        Task Delete(Guid id);
    }
}
