using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class PaymentStatusMockData
    {
        public static List<Data.Models.PaymentStatus> GetPaymentStatuses()
        {
            return new List<Data.Models.PaymentStatus>
            {
                 new Data.Models.PaymentStatus{
                     Id = 1,
                     Description = "Approved"
                 },
                 new Data.Models.PaymentStatus{
                     Id = 2,
                     Description = "Decline"
                 }
            };
        }

        public static List<Data.Models.PaymentStatus> GetEmptyTodos()
        {
            return new List<Data.Models.PaymentStatus>();
        }

        public static PaymentStatusViewModel NewPaymentStatus()
        {
            return new PaymentStatusViewModel
            {
                Description = "Hot Card"
            };
        }

        public static PaymentStatusViewModel NewPaymentStatusWithMoreThan50Characters()
        {
            return new PaymentStatusViewModel
            {
                Description = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Description, which is not correct because in PaymentStatus model, we have defined the size of the Description as 50 characters"
            };
        }
    }
}
