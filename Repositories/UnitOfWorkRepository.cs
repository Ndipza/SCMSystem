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
        private IPaymentMethodRepository _paymentMethodRepository;
        private IPaymentRepository _paymentRepository;
        private IProductRepository _productRepository;
        private ICartStatusRepository _cartStatusRepository;
        private IPaymentStatusRepository _paymentStatusRepository;
        private ICartItemRepository _cartItemRepository;
        public UnitOfWorkRepository(SCMSystemDBContext context, ICartRepository cartRepository,
            ICategoryRepository categoryRepository, IPaymentMethodRepository paymentMethodRepository, IPaymentRepository paymentRepository,
            IProductRepository productRepository, ICartStatusRepository cartStatusRepository, 
            IPaymentStatusRepository paymentStatusRepository, ICartItemRepository cartItemRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
            _categoryRepository = categoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _cartStatusRepository = cartStatusRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _cartItemRepository = cartItemRepository;
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

        public ICartStatusRepository CartStatusRepository
        {
            get
            {
                return _cartStatusRepository ?? new CartStatusRepository(_context);
            }
        }

        public IPaymentStatusRepository PaymentStatusRepository
        {
            get
            {
                return _paymentStatusRepository ?? new PaymentStatusRepository(_context);
            }
        }

        public ICartItemRepository CartItemRepository
        {
            get
            {
                return _cartItemRepository ?? new CartItemRepository(_context);
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
