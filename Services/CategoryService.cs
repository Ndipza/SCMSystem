using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CategoryService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreateCategory(CategoryViewModel categoryViewModel)
        {
            return await _unitOfWork.CategoryRepository.CreateCategory(categoryViewModel);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await _unitOfWork.CategoryRepository.DeleteCategory(id);
        }

        public async Task<List<Category?>?> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetCategories();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _unitOfWork.CategoryRepository.GetCategoryById(id);
        }

        public async Task<Category?> UpdateCategory(CategoryViewModel categoryViewModel, int id)
        {
            return await _unitOfWork.CategoryRepository.UpdateCategory(categoryViewModel, id);
        }
    }
}
