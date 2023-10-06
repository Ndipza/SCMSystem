using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class CartMockData
    {
        public static List<Data.Models.Cart> GetCarts()
        {
            return new List<Data.Models.Cart>
            {
                 new Data.Models.Cart{
                     Id = 1,
                     DateCreated = DateTime.Now,
                     CustomerId = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79"),
                     CartStatusId = 1
                 },
                 new Data.Models.Cart{
                     Id = 2,
                     DateCreated = DateTime.Now,
                     CustomerId = new Guid("14e6b812-2b4d-4004-a5d8-e9dc72cd25dc"),
                     CartStatusId = 2
                 },
                 new Data.Models.Cart{
                     Id = 3,
                     DateCreated = DateTime.Now,
                     CustomerId = new Guid("aadfb272-352f-41dd-bb9d-d3eb36af1c04"),
                     CartStatusId = 2
                 }
            };
        }

        public static List<Data.Models.Cart> GetEmptyTodos()
        {
            return new List<Data.Models.Cart>();
        }

        public static CartViewModel NewCart()
        {
            return new CartViewModel
            {
                CartStatusId = 2,
                CustomerId = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79")
            };
        }
                
    }
}
