using Data;
using Data.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly SCMSystemDBContext _context;
        private ICartService _cartService;
        private ICategoryService _categoryService;
        private ICustomerService _customerService;
        private IPaymentMethodService _paymentMethodService;
        private IPaymentService _paymentService;
        private IProductService _productService;
        private ICustomerStatusService _customerStatusService;
        private ICartStatusService _cartStatusService;
        private IPaymentStatusService _paymentStatusService;
        public UnitOfWorkService(SCMSystemDBContext context, ICartService cartService,
            ICategoryService categoryService, ICustomerService customerService, IPaymentMethodService paymentMethodService, IPaymentService paymentService,
            IProductService productService, ICustomerStatusService customerStatusService,
            ICartStatusService cartStatusService, IPaymentStatusService paymentStatusService)
        {
            _context = context;
            _cartService = cartService;
            _categoryService = categoryService;
            _customerService = customerService;
            _paymentMethodService = paymentMethodService;
            _paymentService = paymentService;
            _productService = productService;
            _customerStatusService = customerStatusService;
            _cartStatusService = cartStatusService;
            _paymentStatusService = paymentStatusService;
        }

        public ICartService CartService
        {
            get
            {
                return _cartService ?? new CartService(_context);
            }
        }

        public ICategoryService CategoryService
        {
            get
            {
                return _categoryService ?? new CategoryService(_context);
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return _customerService ?? new CustomerService(_context);
            }
        }

        public IPaymentMethodService PaymentMethodService
        {
            get
            {
                return _paymentMethodService ?? new PaymentMethodService(_context);
            }
        }

        public IPaymentService PaymentService
        {
            get
            {
                return _paymentService ?? new PaymentService(_context);
            }
        }

        public IProductService ProductService
        {
            get
            {
                return _productService ?? new ProductService(_context);
            }
        }

        public ICustomerStatusService CustomerStatusService
        {
            get
            {
                return _customerStatusService ?? new CustomerStatusService(_context);
            }
        }

        public ICartStatusService CartStatusService
        {
            get
            {
                return _cartStatusService ?? new CartStatusService(_context);
            }
        }

        public IPaymentStatusService PaymentStatusService
        {
            get
            {
                return _paymentStatusService ?? new PaymentStatusService(_context);
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
