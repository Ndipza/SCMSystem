using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SCMSystemDBContext _context;
        public ProductRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var product = GetById(id)?.Result ?? new Product();
            _context.Products.RemoveRange(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(product => product.CartItems)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(ProductViewModel productViewModel)
        {
            var product = new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Product> UpdateAsync(ProductViewModel productViewModel, int id)
        {
            var product = GetById(id)?.Result;

            if (product == null) { return new Product(); }

            product = new Product
            {
                Id = id,
                Name = productViewModel.Name,
                Price = productViewModel.Price
            };
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
