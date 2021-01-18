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
    public class RentReturnRepository : IRentReturnRepository
    {
        private readonly ApplicationDbContext _context;

        public RentReturnRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RentReturn>> GetListAsync()
        {
            return await _context.RentReturns
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RentReturn> FindByIdAsync(int id)
        {
            return await _context.RentReturns
                .Where(x=>x.RentReturnID==id)
                .Include(r => r.RentReturnDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task CreateAsync(RentReturn rentReturn)
        {
            _context.AddRange(rentReturn);
            await _context.SaveChangesAsync();
        }


    }
}
