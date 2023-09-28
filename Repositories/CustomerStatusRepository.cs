using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class CustomerStatusRepository : ICustomerStatusRepository
    {
        private readonly SCMSystemDBContext _context;
        public CustomerStatusRepository(SCMSystemDBContext context)
        {
            _context = context;
        }

        public async Task<long> CreateCustomerStatusAsync(CustomerStatusViewModel customerStatusViewModel)
        {
            var customerStatus = new CustomerStatus
            {
                Description = customerStatusViewModel.Description
            };
            await _context.CustomerStatuses.AddAsync(customerStatus).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return customerStatus.Id;
        }

        public async Task DeleteCustomerStatusById(int id)
        {
            var customerStatus = GetCustomerStatusById(id)?.Result ?? new CustomerStatus();
            _context.CustomerStatuses.RemoveRange(customerStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerStatus>> GetAllCustomerStatuses()
        {
            return await _context.CustomerStatuses.ToListAsync();
        }

        public async Task<CustomerStatus?> GetCustomerStatusById(int id)
        {
            return await _context.CustomerStatuses
                .AsNoTracking()
                .Include(payment => payment.Customers)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CustomerStatus> UpdateCustomerStatusAsync(CustomerStatusViewModel customerStatusViewModel, int id)
        {
            var customerStatus = GetCustomerStatusById(id)?.Result;

            if (customerStatus == null) { return new CustomerStatus(); }

            customerStatus = new CustomerStatus
            {
                Id = id,
                Description = customerStatusViewModel.Description
            };
            _context.CustomerStatuses.Update(customerStatus);
            await _context.SaveChangesAsync();
            return customerStatus;
        }
    }
}
