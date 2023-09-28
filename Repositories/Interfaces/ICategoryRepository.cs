using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetById(int id);
        Task<long> Create(CategoryViewModel categoryViewModel);
        Task<Category> Update(CategoryViewModel categoryViewModel, int id);
        Task Delete(int id);
    }
}
