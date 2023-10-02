using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class CustomerMockData
    {
        public static List<Data.Models.Customer> GetCustomers()
        {
            return new List<Data.Models.Customer>
            {
                 new Data.Models.Customer{
                     Id = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79"),
                     Name = "Sindiswa",
                     CustomerStatusId = 1,
                     CellNumber = "0728452107",
                     Email = "test@test.com",
                     DateCreated = DateTime.Now,
                     DateUpdated = null
                 },
                 new Data.Models.Customer{
                     Id = new Guid("14e6b812-2b4d-4004-a5d8-e9dc72cd25dc"),
                     Name = "Namhla",
                     CustomerStatusId = 2,
                     CellNumber = "0728452107",
                     Email = "test@test.com",
                     DateCreated = DateTime.Now,
                     DateUpdated = null
                 },
                 new Data.Models.Customer{
                     Id = new Guid("aadfb272-352f-41dd-bb9d-d3eb36af1c04"),
                     Name = "Ncedo",
                     CustomerStatusId = 1,
                     CellNumber = "0728452107",
                     Email = "test@test.com",
                     DateCreated = DateTime.Now,
                     DateUpdated = null
                 }
            };
        }

        public static List<Data.Models.Customer> GetEmptyTodos()
        {
            return new List<Data.Models.Customer>();
        }

        public static CustomerViewModel NewCustomer()
        {
            return new CustomerViewModel
            {
                Name = "Luxolo",
                CustomerStatusId = 2,
                CellNumber = "0728452107",
                Email = "test@test.com"
            };
        }

        public static CustomerViewModel NewCustomerWithMoreThan50Characters()
        {
            return new CustomerViewModel
            {
                CustomerStatusId = 3,
                CellNumber = "0728452107",
                Email = "test@test.com",
                Name = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Name, which is not correct because in Customer model, we have defined the size of the Name as 50 characters"
            };
        }
    }
}
