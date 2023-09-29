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
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PaymentMethodService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
        {
            return await _unitOfWork.PaymentMethodRepository.InsertAsync(paymentMethodViewModel);
        }

        public async Task DeletePaymentMethod(int id)
        {
            await _unitOfWork.PaymentMethodRepository.Delete(id);
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethods()
        {
            return await _unitOfWork.PaymentMethodRepository.GetAll();
        }

        public async Task<PaymentMethod?> GetPaymentMethodById(int id)
        {
            return await _unitOfWork.PaymentMethodRepository.GetById(id);
        }

        public async Task<PaymentMethod> UpdatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel, int id)
        {
            return await _unitOfWork.PaymentMethodRepository.UpdateAsync(paymentMethodViewModel, id);
        }
    }
}
