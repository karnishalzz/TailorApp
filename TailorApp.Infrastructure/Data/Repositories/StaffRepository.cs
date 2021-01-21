using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;
        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Staff> Staffs => _context.Staff.AsQueryable();

        public async Task CreateAsync(Staff Staff)
        {
            _context.Staff.Add(Staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Staff Staff)
        {
            _context.Staff.Remove(Staff);
            await _context.SaveChangesAsync();
        }

        public async Task<Staff> FindByIdAsync(int? id)
        {
            return await _context.Staff
                .FirstOrDefaultAsync(x => x.StaffID== id);
        }

        public async Task<List<Staff>> GetListAsync()
        {
            return await _context.Staff
                .AsNoTracking()
                .ToListAsync();
        }

        public bool IsExists(int id)
        {
            return _context.Staff.Any(e => e.StaffID == id);
        }

        public async Task UpdateAsync(Staff Staff)
        {
            _context.Staff.Update(Staff);
            await _context.SaveChangesAsync();
        }
    }
}
