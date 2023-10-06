using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CartService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreateCart(CartViewModel cartViewModel)
        {
            return await _unitOfWork.CartRepository.CreateCart(cartViewModel);
        }

        public async Task<bool> DeleteCart(int id, string? userId)
        {
            return await _unitOfWork.CartRepository.DeleteCart(id, userId);
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await _unitOfWork.CartRepository.GetAllCarts();
        }

        public async Task<Cart?> GetCartById(int id)
        {
            return await _unitOfWork.CartRepository.GetCartById(id);
        }

        public async Task<Cart> UpdateCart(CartViewModel cartViewModel, int id)
        {
            return await _unitOfWork.CartRepository.UpdateCart(cartViewModel, id);
        }
    }
}
