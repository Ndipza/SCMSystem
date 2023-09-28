using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SCMSystemDBContext _context;
        public CategoryRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task DeleteCategory(int id)
        {
            if (_context != null)
            {
                var category = GetCategoryById(id)?.Result ?? new Category();
                _context.Categories.RemoveRange(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            if (_context == null)
                return new List<Category>();

            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            if (_context == null)
                return new Category();

            return await _context.Categories
                .AsNoTracking()
                .Include(category => category.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (_context == null)
                return 0;

            var category = new Category
            {
                Name = categoryViewModel.Name
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<Category> UpdateCategory(CategoryViewModel categoryViewModel, int id)
        {
            var category = GetCategoryById(id)?.Result;

            if (category == null) { return new Category(); }

            category = new Category
            {
                Id = id,
                Name = categoryViewModel.Name,
                Products = category.Products.ToList()
            };
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}