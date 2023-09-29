using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUnitOfWorkService
    {
        
        ICartService CartService { get; }
        ICategoryService CategoryService { get; }
        ICustomerService CustomerService { get; }
        IPaymentMethodService PaymentMethodService { get; }
        IPaymentService PaymentService { get; }
        IProductService ProductService { get; }
        ICustomerStatusService CustomerStatusService { get; }
        ICartStatusService CartStatusService { get; }
        IPaymentStatusService PaymentStatusService { get; }
        void Save();
        Task SaveAsync();
    }
}
