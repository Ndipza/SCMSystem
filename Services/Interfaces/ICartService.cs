using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface ICartService
    {
        Task<List<Cart>> GetAll();
        Task<Cart?> GetById(int id);
        Task<long> InsertAsync(CartViewModel categoryViewModel);
        Task<Cart> UpdateAsync(CartViewModel categoryViewModel, int id);
        Task Delete(int id);
    }
}
