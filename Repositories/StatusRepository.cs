using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly SCMSystemDBContext _context;
        public StatusRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var status = GetById(id)?.Result ?? new Status();
            _context.Statuses.RemoveRange(status);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Status>> GetAll()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status?> GetById(int id)
        {
            return await _context.Statuses
                .AsNoTracking()
                .Include(status => status.OrderItems)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(StatusViewModel categoryViewModel)
        {
            var status = new Status
            {
                Name = categoryViewModel.Name
            };
            await _context.Statuses.AddAsync(status);
            return status.Id;
        }

        public async Task<Status> UpdateAsync(StatusViewModel statusViewModel, int id)
        {
            var status = GetById(id)?.Result;

            if (status == null) { return new Status(); }

            status = new Status
            {
                Id = id,
                Name = statusViewModel.Name
            };
            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
