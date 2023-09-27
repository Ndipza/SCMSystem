using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SCMSystemDBContext _context;
        public OrderRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var category = GetById(id)?.Result ?? new Order();
            _context.Orders.RemoveRange(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(order => order.Customer)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(OrderViewModel orderViewModel)
        {
            var order = new Order
            {
                CustomerId = orderViewModel.CustomerId,
                ProductId = orderViewModel.ProductId,
                Quantity = orderViewModel.Quantity
            };
            await _context.Orders.AddAsync(order);
            return order.Id;
        }

        public async Task<Order> UpdateAsync(OrderViewModel orderViewModel, int id)
        {
            var order = GetById(id)?.Result;

            if (order == null) { return new Order(); }

            order = new Order
            {
                Id = id,
                CustomerId = orderViewModel.CustomerId,
                ProductId = orderViewModel.ProductId,
                Quantity = orderViewModel.Quantity
            };
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
