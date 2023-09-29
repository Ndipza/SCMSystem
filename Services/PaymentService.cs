using Core.ViewModels;
using Data.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PaymentService(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreatePayment(PaymentViewModel paymentViewModel)
        {
            return await _unitOfWork.PaymentRepository.CreatePayment(paymentViewModel);
        }

        public async Task DeletePayment(int id)
        {
            await _unitOfWork.PaymentRepository.DeletePayment(id);
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _unitOfWork.PaymentRepository.GetAllPayments();
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            return await _unitOfWork.PaymentRepository.GetPaymentById(id);
        }

        public async Task<Payment> UpdatePayment(PaymentViewModel paymentViewModel, int id)
        {
            return await _unitOfWork.PaymentRepository.UpdatePayment(paymentViewModel, id);
        }
    }
}
