using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class CartStatusService : ICartStatusService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CartStatusService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<long> CreateCartStatusAsync(CartStatusViewModel cartStatusViewModel)
        {
            return await _unitOfWork.CartStatusRepository.CreateCartStatusAsync(cartStatusViewModel);
        }

        public async Task DeleteCartStatusById(int id)
        {
            await _unitOfWork.CartStatusRepository.DeleteCartStatusById(id);
        }

        public async Task<List<CartStatus>> GetAllCartStatuses()
        {
            return await _unitOfWork.CartStatusRepository.GetAllCartStatuses();
        }

        public async Task<CartStatus?> GetCartStatusById(int id)
        {
            return await _unitOfWork.CartStatusRepository.GetCartStatusById(id);
        }

        public async Task<CartStatus> UpdateCartStatusAsync(CartStatusViewModel cartStatusViewModel, int id)
        {
            return await _unitOfWork.CartStatusRepository.UpdateCartStatusAsync(cartStatusViewModel, id);
        }
    }
}
