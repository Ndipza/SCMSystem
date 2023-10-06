using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        
        ICartRepository CartRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPaymentMethodRepository PaymentMethodRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        ICartStatusRepository CartStatusRepository { get; }
        IPaymentStatusRepository PaymentStatusRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
