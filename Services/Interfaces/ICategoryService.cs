using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategoryById(int id);
        Task<long> CreateCategory(CategoryViewModel categoryViewModel);
        Task<Category> UpdateCategory(CategoryViewModel categoryViewModel, int id);
        Task<bool> DeleteCategory(int id);
    }
}
