using Core.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartStatusServices
    {
        Task<List<CartStatus>> GetAllCartStatuses();
        Task<CartStatus?> GetCartStatusById(int id);
        Task<long> CreateCartStatusAsync(CartStatusViewModel cartStatusViewModel);
        Task<CartStatus> UpdateCartStatusAsync(CartStatusViewModel cartStatusViewModel, int id);
        Task DeleteCartStatusById(int id);
    }
}
