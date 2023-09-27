using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task<long> InsertAsync(CategoryViewModel categoryViewModel);
        Task<Category> UpdateAsync(CategoryViewModel categoryViewModel, int id);
        Task Delete(int id);
    }
}
