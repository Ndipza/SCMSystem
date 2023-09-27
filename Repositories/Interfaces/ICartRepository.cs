using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAll();
        Task<Cart?> GetById(int id);
        Task<long> InsertAsync(CartViewModel categoryViewModel);
        Task<Cart> UpdateAsync(CartViewModel categoryViewModel, int id);
        Task Delete(int id);
    }
}
