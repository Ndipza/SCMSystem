using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SCMSystemDBContext _context;
        public OrderItemRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var orderItem = GetById(id)?.Result ?? new OrderItem();
            _context.OrderItems.RemoveRange(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderItem>> GetAll()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem?> GetById(int id)
        {
            return await _context.OrderItems
                .AsNoTracking()
                .Include(orderItem => orderItem.Order)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(OrderItemViewModel orderItemViewModel)
        {
            var orderItem = new OrderItem
            {
                Name = orderItemViewModel.Name
            };
            await _context.OrderItems.AddAsync(orderItem);
            return orderItem.Id;
        }

        public async Task<OrderItem> UpdateAsync(OrderItemViewModel orderItemViewModel, int id)
        {
            var orderItem = GetById(id)?.Result;

            if (orderItem == null) { return new OrderItem(); }

            orderItem = new OrderItem
            {
                Id = id,
                Name = orderItemViewModel.Name,
                Order = orderItem.Order
            };
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
    }
}
