using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public ProductService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreateProduct(ProductViewModel productViewModel)
        {
            return await _unitOfWork.ProductRepository.CreateProduct(productViewModel);
        }

        public async Task DeleteProduct(int id)
        {
            await _unitOfWork.ProductRepository.DeleteProduct(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _unitOfWork.ProductRepository.GetAllProducts();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _unitOfWork.ProductRepository.GetProductById(id);
        }

        public async Task<Product> UpdateProduct(ProductViewModel productViewModel, int id)
        {
            return await _unitOfWork.ProductRepository.UpdateProduct(productViewModel, id);
        }
    }
}
