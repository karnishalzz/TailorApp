using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Stock> Stocks => _context.Stocks.AsQueryable();

        public async Task<List<Stock>> GetListAsync()
        {
            return await _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Purchase)
                .AsNoTracking()
                .ToListAsync();
        }
       
        public async Task<Stock> FindByIdAsync(int? id)
        {
            return await _context.Stocks
                .Where(x => x.StockID == id)
                .Include(s => s.Item)
                .Include(s => s.Purchase)
                .ThenInclude(p => p.Supplier)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Stock>> GetByItemCategory(int itemId, CategoryType category)
        {
            return await _context.Stocks
                .Where(x => x.ItemID == itemId && x.Category == category)
                .ToListAsync();
               
        }

        public async Task CreateAsync(Stock stock)
        {
            _context.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockListAsync(List<Stock> stocks)
        {
            _context.UpdateRange(stocks);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Stock>> GetListByCategoryAsync(CategoryType categoryType)
        {
          return await _context.Stocks
                .Where(x=>x.Category==categoryType)
                .OrderBy(i => i.Item.Name)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
