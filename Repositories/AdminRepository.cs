using Core.ViewModels;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SCMSystemDBContext _context;
        public AdminRepository(SCMSystemDBContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var admin = GetById(id)?.Result ?? new Admin();
            _context.Admins.RemoveRange(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Admin>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin?> GetById(int id)
        {
            return await _context.Admins
                .AsNoTracking()
                .Include(admin => admin.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> InsertAsync(AdminViewModel adminViewModel)
        {
            var admin = new Admin
            {
                Name = adminViewModel.Name
            };
            await _context.Admins.AddAsync(admin);
            return admin.Id;
        }

        public async Task<Admin> UpdateAsync(AdminViewModel adminViewModel, int id)
        {
            var admin = GetById(id)?.Result;

            if (admin == null) { return new Admin(); }

            admin = new Admin
            {
                Id = id,
                Name = adminViewModel.Name,
                Products = admin.Products.ToList()
            };
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
