using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllCarts();
        Task<Cart?> GetCartById(int id);
        Task<long> CreateCart(CartViewModel cartViewModel);
        Task<Cart> UpdateCart(CartViewModel cartViewModel, int id);
        Task DeleteCart(int id);
    }
}
