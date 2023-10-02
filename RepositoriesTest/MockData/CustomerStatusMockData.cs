using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class CustomerStatusMockData
    {

        public static List<Data.Models.CustomerStatus> GetCustomerStatuses()
        {
            return new List<Data.Models.CustomerStatus>
            {
                 new Data.Models.CustomerStatus{
                     Id = 1,
                     Description = "Active"
                 },
                 new Data.Models.CustomerStatus{
                     Id = 2,
                     Description = "InActive"
                 }
            };
        }

        public static List<Data.Models.CustomerStatus> GetEmptyTodos()
        {
            return new List<Data.Models.CustomerStatus>();
        }

        public static CustomerStatusViewModel NewCustomerStatus()
        {
            return new CustomerStatusViewModel
            {
                Description = "Blacklisted"
            };
        }

        public static CustomerStatusViewModel NewCustomerStatusWithMoreThan50Characters()
        {
            return new CustomerStatusViewModel
            {
                Description = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Description, which is not correct because in CustomerStatus model, we have defined the size of the Description as 50 characters"
            };
        }
    }
}
