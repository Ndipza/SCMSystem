using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItem>> GetAllCartItems();
        Task<CartItem?> GetCartItemById(int id);
        Task<long> CreateCartItem(CartItemViewModel cartItemViewModel);
        Task<CartItem> UpdateCartItem(CartItemViewModel cartItemViewModel, int id);
        Task<bool> DeleteCartItem(int id);
    }
}
