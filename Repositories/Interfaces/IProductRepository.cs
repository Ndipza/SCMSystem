using Core.ViewModels;
using Data.DTO;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<long> CreateProduct(ProductViewModel productViewModel);
        Task<Product> UpdateProduct(ProductViewModel productViewModel, int id);
        Task<DeletedProduct> DeleteProduct(int id);
    }
}
