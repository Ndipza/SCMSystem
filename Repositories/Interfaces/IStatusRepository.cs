using Core.ViewModels;
using Data.Models;

namespace Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAll();
        Task<Status?> GetById(int id);
        Task<long> InsertAsync(StatusViewModel statusViewModel);
        Task<Status> UpdateAsync(StatusViewModel statusViewModel, int id);
        Task Delete(int id);
    }
}
