using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetAll();
        Task<Admin?> GetById(int id);
        Task<long> InsertAsync(AdminViewModel adminViewModel);
        Task<Admin> UpdateAsync(AdminViewModel adminViewModel, int id);
        Task Delete(int id);
    }
}
