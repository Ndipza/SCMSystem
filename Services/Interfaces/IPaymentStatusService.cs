using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentStatusService
    {
        Task<List<PaymentStatus>> GetAllPaymentStatuses();
        Task<PaymentStatus?> GetPaymentStatusById(int id);
        Task<long> CreatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel);
        Task<PaymentStatus> UpdatePaymentStatusAsync(PaymentStatusViewModel paymentStatusViewModel, int id);
        Task<bool> DeletePaymentStatusById(int id);
    }
}
