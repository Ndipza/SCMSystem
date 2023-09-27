using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAll();
        Task<OrderItem?> GetById(int id);
        Task<long> InsertAsync(OrderItemViewModel categoryViewModel);
        Task<OrderItem> UpdateAsync(OrderItemViewModel categoryViewModel, int id);
        Task Delete(int id);
    }
}
