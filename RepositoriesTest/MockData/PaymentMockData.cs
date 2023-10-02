using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class PaymentMockData
    {
        public static List<Data.Models.Payment> GetPayments()
        {
            return new List<Data.Models.Payment>
            {
                 new Data.Models.Payment{
                     Id = 1,
                     PaymentDate = DateTime.Now,
                     CartId = 3,
                     PaymentMethodId = 3,
                     PaymentStatusId = 4,
                     Balance = 30
                 },
                 new Data.Models.Payment{
                     Id = 2,
                     PaymentDate = DateTime.Now,
                     CartId = 4,
                     PaymentMethodId = 3,
                     PaymentStatusId = 3,
                     Balance = 3545
                 },
                 new Data.Models.Payment{
                     Id = 3,
                     PaymentDate = DateTime.Now,
                     CartId = 2,
                     PaymentMethodId = 3,
                     PaymentStatusId = 3,
                     Balance = 343
                 },
                 new Data.Models.Payment{
                     Id = 4,
                     PaymentDate = DateTime.Now,
                     CartId = 3,
                     PaymentMethodId = 3,
                     PaymentStatusId = 3,
                     Balance = 3245
                 },
                 new Data.Models.Payment{
                     Id = 5,
                     PaymentDate = DateTime.Now,
                     CartId = 2,
                     PaymentMethodId = 3,
                     PaymentStatusId = 3,
                     Balance = 4234
                 },
                 new Data.Models.Payment{
                     Id = 6,
                     PaymentDate = DateTime.Now,
                     CartId = 2,
                     PaymentMethodId = 2,
                     PaymentStatusId = 2,
                     Balance = 2343
                 }
            };
        }

        public static List<Data.Models.Payment> GetEmptyTodos()
        {
            return new List<Data.Models.Payment>();
        }

        public static PaymentViewModel NewPayment()
        {
            return new PaymentViewModel
            {
                PaymentMethodID = 2,
                CartId = 1,
                PaymentStatusId = 2,
                Amount = 244
            };
        }

    }
}
