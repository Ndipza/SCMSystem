using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetAllCartItems(string? userId);
        Task<CartItem?> GetCartItemById(int id, string? userId);
        Task<long?> CreateCartItem(CartItemViewModel cartItemViewModel, string? userId);
        Task<CartItem?> UpdateCartItem(CartItemViewModel cartItemViewModel, int id, string? userId);
        Task<bool> DeleteCartItem(int id, string? userId);
    }
}
