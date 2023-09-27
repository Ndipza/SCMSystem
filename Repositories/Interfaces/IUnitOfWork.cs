using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IAdminRepository AdminRepository { get; }
        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IPaymentMethodRepository PaymentMethodRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        IStatusRepository StatusRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
