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
        public async Task<bool> DeleteCustomer(Guid id)
        {
            if (_context != null)
            {
                var customer = GetCustomerById(id)?.Result;
                if (customer != null)
                {
                    _context.Customers.RemoveRange(customer);
                    await _context.SaveChangesAsync();

                    return true;
                }

            }
            return false;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers
                .Include(customer => customer.Carts)
                .Include(customer => customer.CustomerStatus).ToListAsync();
        }

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await _context.Customers
                .Include(customer => customer.Carts)
                .Include(customer => customer.CustomerStatus)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> CreateCustomer(CustomerViewModel customerViewModel)
        {
            var customer = new Customer
            {
                Name = customerViewModel.Name,
                Email = customerViewModel.Email,   
                CellNumber = customerViewModel.CellNumber,
                DateCreated = DateTime.Now,
                CustomerStatusId = customerViewModel.CustomerStatusId,
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<Customer> UpdateCustomers(CustomerViewModel customerViewModel, Guid id)
        {
            var customer = GetCustomerById(id)?.Result;

            if (customer == null) { return new Customer(); }

            customer.Id = id;
            customer.Name = customerViewModel.Name;
            customer.Email = customerViewModel.Email;
            customer.CellNumber = customerViewModel.CellNumber;
            customer.DateUpdated = DateTime.Now;
            customer.CustomerStatusId = customerViewModel.CustomerStatusId;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
