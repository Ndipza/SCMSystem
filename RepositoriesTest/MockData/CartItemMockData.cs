using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class CartItemMockData
    {
        public static List<Data.Models.CartItem> GetCartItems()
        {

            return new List<Data.Models.CartItem>
            {
                 new Data.Models.CartItem{
                     Id = 1,
                     CartId = 1,
                     ProductId = 1,
                     Quantity = 10
                 },
                 new Data.Models.CartItem{
                     Id = 2,
                     CartId = 2,
                     ProductId = 1,
                     Quantity = 20
                 },
                 new Data.Models.CartItem{
                     Id = 3,
                     CartId = 2,
                     ProductId = 2,
                     Quantity = 30
                 },
                 new Data.Models.CartItem{
                     Id = 4,
                     CartId = 1,
                     ProductId = 1,
                     Quantity = 40
                 },
                 new Data.Models.CartItem{
                     Id = 5,
                     CartId = 3,
                     ProductId = 1,
                     Quantity = 50
                 },
                 new Data.Models.CartItem{
                     Id = 6,
                     CartId = 1,
                     ProductId = 3,
                     Quantity = 60
                 }
            };
        }

        public static List<Data.Models.CartItem> GetEmptyTodos()
        {
            return new List<Data.Models.CartItem>();
        }

        public static CartItemViewModel NewCartItem()
        {
            return new CartItemViewModel
            {

                CartId = 4,
                ProductId = 3,
                Quantity = 5
            };
        }

        public static CartItemViewModel InvalidCartItem()
        {
            return new CartItemViewModel
            {

                CartId = 4,
                ProductId = 3,
                Quantity = 10101
            };
        }


    }
}
