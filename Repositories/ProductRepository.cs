using Core.ViewModels;
using Data;
using Data.DTO;
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
        public async Task<DeletedProduct> DeleteProduct(int id)
        {
            var deletedProduct = new DeletedProduct();
            if (_context != null)
            {
                var product = GetProductById(id)?.Result;
                if(product != null)
                {
                    _context.Products.RemoveRange(product);
                    await _context.SaveChangesAsync();

                    deletedProduct.IsProductDeleted = true;
                    deletedProduct.ImageName = product.Image;
                }
                
            }
            return deletedProduct;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var image = _context.Products;
            return await _context.Products
                .Include(p=>p.CartItems)
                .Include(p=>p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products
                .Include(product => product.CartItems)
                .Include(product => product.Category)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> CreateProduct(ProductViewModel productViewModel)
        {
            var product = new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Image = productViewModel.ImageName,
                CategoryId = productViewModel.CategoryId
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Product> UpdateProduct(ProductViewModel productViewModel, int id)
        {
            var product = GetProductById(id)?.Result;

            if (product == null) { return new Product(); }

            product.Id = id;
            product.Name = productViewModel.Name;
            product.Price = productViewModel.Price;
            product.Image = productViewModel.ImageName;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
