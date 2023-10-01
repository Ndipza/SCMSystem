using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetAllCartItems();
        Task<CartItem?> GetCartItemById(int id);
        Task<long> CreateCartItem(CartItemViewModel cartItemViewModel);
        Task<CartItem> UpdateCartItem(CartItemViewModel cartItemViewModel, int id);
        Task<bool> DeleteCartItem(int id);
    }
}
