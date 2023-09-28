using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SCMSystemDBContext _context;
        public CustomerRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(Guid id)
        {
            var customer = GetById(id)?.Result ?? new Customer();
            _context.Customers.RemoveRange(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(customer => customer.Carts)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(CustomerViewModel customerViewModel)
        {
            var customer = new Customer
            {
                Name = customerViewModel.Name,
                Email = customerViewModel.Email,   
                CellNumber = customerViewModel.CellNumber,
                DateCreated = DateTime.Now
            };
            await _context.Customers.AddAsync(customer);
            return customer.Id;
        }

        public async Task<Customer> UpdateAsync(CustomerViewModel customerViewModel, Guid id)
        {
            var customer = GetById(id)?.Result;

            if (customer == null) { return new Customer(); }

            customer = new Customer
            {
                Id = id,
                Name = customerViewModel.Name,
                Email = customerViewModel.Email,
                CellNumber = customerViewModel.CellNumber,
                DateUpdated = DateTime.Now
            };
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
