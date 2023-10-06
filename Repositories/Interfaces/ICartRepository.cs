using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllCarts();
        Task<Cart?> GetCartById(int id);
        Task<long> CreateCart(CartViewModel cartViewModel);
        Task<Cart> UpdateCart(CartViewModel cartViewModel, int id);
        Task<bool> DeleteCart(int id, string? userId);
    }
}
