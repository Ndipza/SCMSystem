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

        public async Task<long?> CreateCartItem(CartItemViewModel cartItemViewModel, string? userId)
        {
            return await _unitOfWork.CartItemRepository.CreateCartItem(cartItemViewModel, userId);
        }

        public async Task<bool> DeleteCartItem(int id, string? userId)
        {
            return await _unitOfWork.CartItemRepository.DeleteCartItem(id, userId);
        }

        public async Task<List<CartItem>> GetAllCartItems(string? userId)
        {
            return await _unitOfWork.CartItemRepository.GetAllCartItems(userId);
        }

        public async Task<CartItem?> GetCartItemById(int id, string? userId)
        {
            return await _unitOfWork.CartItemRepository.GetCartItemById(id, userId);
        }

        public async Task<CartItem?> UpdateCartItem(CartItemViewModel cartItemViewModel, int id, string? userId)
        {
            return await _unitOfWork.CartItemRepository.UpdateCartItem(cartItemViewModel, id, userId);
        }
    }
}
