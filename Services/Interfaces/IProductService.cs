using Core.ViewModels;
using Data.Models;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<long> InsertAsync(ProductViewModel productViewModel);
        Task<Product> UpdateAsync(ProductViewModel productViewModel, int id);
        Task Delete(int id);
    }
}
