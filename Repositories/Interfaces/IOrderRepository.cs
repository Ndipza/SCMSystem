using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order?> GetById(int id);
        Task<long> InsertAsync(OrderViewModel orderViewModel);
        Task<Order> UpdateAsync(OrderViewModel orderViewModel, int id);
        Task Delete(int id);
    }
}
