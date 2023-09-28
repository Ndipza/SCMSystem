using Data;
using Data.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly SCMSystemDBContext _context;
        private ICartRepository _cartRepository;
        private ICategoryRepository _categoryRepository;
        private ICustomerRepository _customerRepository;
        private IPaymentMethodRepository _paymentMethodRepository;
        private IPaymentRepository _paymentRepository;
        private IProductRepository _productRepository;
        public UnitOfWorkRepository(SCMSystemDBContext context, ICartRepository cartRepository,
            ICategoryRepository categoryRepository, ICustomerRepository customerRepository, IPaymentMethodRepository paymentMethodRepository, IPaymentRepository paymentRepository,
            IProductRepository productRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
            _categoryRepository = categoryRepository;
            _customerRepository = customerRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
        }

        public ICartRepository CartRepository
        {
            get
            {
                return _cartRepository ?? new CartRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ?? new CategoryRepository(_context);
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ?? new CustomerRepository(_context);
            }
        }

        public IPaymentMethodRepository PaymentMethodRepository
        {
            get
            {
                return _paymentMethodRepository ?? new PaymentMethodRepository(_context);
            }
        }

        public IPaymentRepository PaymentRepository
        {
            get
            {
                return _paymentRepository ?? new PaymentRepository(_context);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ?? new ProductRepository(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
