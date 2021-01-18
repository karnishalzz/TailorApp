using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.RentModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly ApplicationDbContext _context;

        public RentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Rent> Rents => _context.Rents.AsQueryable();
        public async Task<List<Rent>> GetListAsync()
        {
            return await _context.Rents
                .Include(r => r.Customer)
                .Include(r => r.RentDetails)
                .Include(r => r.RentReturns)
                .ThenInclude(r => r.RentReturnDetails)
                .ToListAsync();
        }
        public async Task<Rent> FindByIdAsync(int? id)
        {
            return await _context.Rents
                .Where(m=>m.RentID==id)
                .Include(e => e.RentDetails)
                .ThenInclude(e => e.Stock)
                .ThenInclude(e => e.Item)
                .FirstOrDefaultAsync();
        }

        public async Task<RentDetail> FindDetailByIdAsync(int id)
        {
            return await _context.RentDetails
                .FirstOrDefaultAsync(m => m.RentDetailID == id);

        }
        public async Task CreateAsync(Rent rent)
        {
            _context.AddRange(rent);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Rent rent)
        {
            _context.AddRange(rent);
            await _context.SaveChangesAsync();
        }
    }
}
