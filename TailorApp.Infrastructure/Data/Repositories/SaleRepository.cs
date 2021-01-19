using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Sales> Sales => _context.Sales.AsQueryable();
        public int Total => _context.SalesDetails.Count();
        public int Monthly => _context.SalesDetails
            .Where(x => DateTime.Compare(x.Sales.Date, DateTime.Today.AddMonths(-1)) >= 0)
            .Sum(x => x.Quantity);
        public async Task CreateAsync(Sales sale)
        {
            _context.Sales.AddRange(sale);
            await _context.SaveChangesAsync();

        }

        public async Task<Sales> FindByIdAsync(int? id)
        {
            return await _context.Sales
                .Where(x => x.SalesID == id)
                .Include(e => e.SalesItems)
                .ThenInclude(e => e.Stock)
                .ThenInclude(e => e.Item)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Sales>> GetByDateAsync(DateTime date)
        {
           return await _context.Sales
                .Where(x => x.Date.Date == date.Date)
                .ToListAsync();
        }

        public object GetByYear(int year)
        {
            return _context.Sales
                .Where(x => x.Date.Year == year)
                .OrderBy(x => x.Date.Month)
                .Select(g => new
                {
                    Month = g.Date.Month,
                    Total = g.GrandTotal
                });
        }

        public object GetByYearAndMonth(int year, int month)
        {
            return _context.Sales
            .Where(x => x.Date.Year == year && x.Date.Month == month)
            .OrderBy(x => x.Date)
            .Select(g => new
                {
                    Day = g.Date.Day,
                    Total = g.GrandTotal
                });
            
        }

        public async Task<List<Sales>> GetListAsync()
        {
            return await _context.Sales
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
