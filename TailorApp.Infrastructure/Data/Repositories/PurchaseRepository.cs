using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.PurchaseModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;
        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Purchase> Purchases => _context.Purchases.AsQueryable();

        public async Task<List<Purchase>> GetListAsync()
        {
            return await _context.Purchases
                .Include(i => i.Supplier)
                 .ToListAsync();
        }

        public async Task<Purchase> FindByIdAsync(int? id)
        {
            return await _context.Purchases
                .Where(x=>x.PurchaseID==id)
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseDetails)
                .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(Purchase Purchase)
        {
            _context.Purchases.UpdateRange(Purchase);
            await _context.SaveChangesAsync();
        }
        public bool DetailIsExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.PurchaseDetailID == id);
        }

       
        public async Task<PurchaseDetail> FindDetailByIdAsync(int id)
        {
            return await _context.PurchaseDetails
                .Where(x => x.PurchaseDetailID == id)
                .Include(c => c.Item)
                .Include(s => s.Purchase)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailAsync(PurchaseDetail purchaseDetail)
        {
            _context.PurchaseDetails.UpdateRange(purchaseDetail);
            await _context.SaveChangesAsync();
        }
    }
}
