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

        public async Task DeletePaymentStatusById(int id)
        {
            await _unitOfWork.PaymentStatusRepository.DeletePaymentStatusById(id);
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
