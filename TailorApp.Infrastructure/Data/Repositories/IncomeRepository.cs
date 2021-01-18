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
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;
        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Income> Incomes => _context.Incomes.AsQueryable();

        public async Task<Income> FindByIdAsync(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public async Task<Income> GetByOrderId(int orderId)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(x => x.OrderID == orderId);
        }
        public async Task<Income> GetByRentId(int rentId)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(x => x.RentID == rentId);
        }

        public async Task<List<Income>> GetListAsync()
        {
            return await _context.Incomes
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task UpdateAsync(Income income)
        {
             _context.Update(income);
            await _context.SaveChangesAsync();
        }
        public async Task CreateAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
        }

        
    }
}
