using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategoryById(int id);
        Task<long> CreateCategory(CategoryViewModel categoryViewModel);
        Task<Category> UpdateCategory(CategoryViewModel categoryViewModel, int id);
        Task DeleteCategory(int id);
    }
}
