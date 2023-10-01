using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICartStatusRepository
    {
        Task<List<CartStatus>> GetAllCartStatuses();
        Task<CartStatus?> GetCartStatusById(int id);
        Task<long> CreateCartStatusAsync(CartStatusViewModel cartStatusViewModel);
        Task<CartStatus> UpdateCartStatusAsync(CartStatusViewModel cartStatusViewModel, int id);
        Task<bool> DeleteCartStatusById(int id);
    }
}
