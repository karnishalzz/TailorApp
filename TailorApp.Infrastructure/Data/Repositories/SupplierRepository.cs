using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Supplier> Suppliers => _context.Suppliers.AsQueryable();

        public async Task CreateAsync(Supplier Supplier)
        {
            _context.Suppliers.Add(Supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task<Supplier> FindByIdAsync(int? id)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierID == id);
        }

        public async Task<List<Supplier>> GetListAsync()
        {
            return await _context.Suppliers
                .AsNoTracking()
                .ToListAsync();
        }

        public bool IsExists(int id)
        {
            return _context.Suppliers.Any(e => e.SupplierID == id);
        }

        public async Task UpdateAsync(Supplier Supplier)
        {
            _context.Suppliers.Update(Supplier);
            await _context.SaveChangesAsync();
        }
        public async Task<SelectList> GetSelectListAsync(int? selectedSupplierId)
        {
            var supplierList = await _context.Suppliers.Select(c => new { c.SupplierID, c.Name })
                .OrderBy(c => c.Name).ToListAsync();

            return new SelectList(supplierList, "SupplierID", "Name", selectedSupplierId);
        }
    }
}
