using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class CartStatusMockData
    {
        public static List<Data.Models.CartStatus> GetCartStatuses()
        {
            return new List<Data.Models.CartStatus>
            {
                 new Data.Models.CartStatus{
                     Id = 1,
                     Description = "Active"
                 },
                 new Data.Models.CartStatus{
                     Id = 2,
                     Description = "Closed"
                 }
            };
        }

        public static List<Data.Models.CartStatus> GetEmptyTodos()
        {
            return new List<Data.Models.CartStatus>();
        }

        public static CartStatusViewModel NewCartStatus()
        {
            return new CartStatusViewModel
            {
                Description = "Stale"
            };
        }

        public static CartStatusViewModel NewCartStatusWithMoreThan50Characters()
        {
            return new CartStatusViewModel
            {
                Description = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Description, which is not correct because in CartStatus model, we have defined the size of the Description as 50 characters"
            };
        }
    }
}
