using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PaymentStatusService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel)
        {
            return await _unitOfWork.PaymentStatusRepository.CreatePaymentStatusAsync(paymentStatusViewModel);
        }

        public async Task<bool> DeletePaymentStatusById(int id)
        {
            return await _unitOfWork.PaymentStatusRepository.DeletePaymentStatusById(id);
        }

        public async Task<List<PaymentStatus>> GetAllPaymentStatuses()
        {   
            return await _unitOfWork.PaymentStatusRepository.GetAllPaymentStatuses();
        }

        public async Task<PaymentStatus?> GetPaymentStatusById(int id)
        {
            return await _unitOfWork.PaymentStatusRepository.GetPaymentStatusById(id);
        }

        public async Task<PaymentStatus> UpdatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel, int id)
        {
            return await _unitOfWork.PaymentStatusRepository.UpdatePaymentStatusAsync(paymentStatusViewModel, id);
        }
    }
}
