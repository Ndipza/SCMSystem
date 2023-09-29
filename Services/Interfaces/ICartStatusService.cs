using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface ICartStatusService
    {
        Task<List<CartStatus>> GetAllCartStatuses();
        Task<CartStatus?> GetCartStatusById(int id);
        Task<long> CreateCartStatusAsync(CartStatusViewModel cartStatusViewModel);
        Task<CartStatus> UpdateCartStatusAsync(CartStatusViewModel cartStatusViewModel, int id);
        Task DeleteCartStatusById(int id);
    }
}
