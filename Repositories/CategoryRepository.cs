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
        public async Task Delete(int id)
        {
            var category = GetById(id)?.Result ?? new Category();
            _context.Categories.RemoveRange(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(category => category.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                Name = categoryViewModel.Name
            };
            await _context.Categories.AddAsync(category);
            return category.Id;
        }

        public async Task<Category> UpdateAsync(CategoryViewModel categoryViewModel, int id)
        {
            var category = GetById(id)?.Result;

            if(category == null) { return new Category(); }

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