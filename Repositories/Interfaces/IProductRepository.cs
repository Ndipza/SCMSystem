using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<long> InsertAsync(ProductViewModel productViewModel);
        Task<Product> UpdateAsync(ProductViewModel productViewModel, int id);
        Task Delete(int id);
    }
}
