using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CartItemService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreateCartItem(CartItemViewModel cartItemViewModel)
        {
            return await _unitOfWork.CartItemRepository.CreateCartItem(cartItemViewModel);
        }

        public async Task DeleteCartItem(int id)
        {
            await _unitOfWork.CartItemRepository.DeleteCartItem(id);
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            return await _unitOfWork.CartItemRepository.GetAllCartItems();
        }

        public async Task<CartItem?> GetCartItemById(int id)
        {
            return await _unitOfWork.CartItemRepository.GetCartItemById(id);
        }

        public async Task<CartItem> UpdateCartItem(CartItemViewModel cartItemViewModel, int id)
        {
            return await _unitOfWork.CartItemRepository.UpdateCartItem(cartItemViewModel, id);
        }
    }
}
