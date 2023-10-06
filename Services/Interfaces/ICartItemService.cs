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
        Task<List<CartItem>> GetAllCartItems(string? userId);
        Task<CartItem?> GetCartItemById(int id, string? userId);
        Task<long?> CreateCartItem(CartItemViewModel cartItemViewModel, string? userId);
        Task<CartItem?> UpdateCartItem(CartItemViewModel cartItemViewModel, int id, string? userId);
        Task<bool> DeleteCartItem(int id, string? userId);
    }
}
