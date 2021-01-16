using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Item> Items => _context.Items.AsQueryable();

        public async Task CreateAsync(Item Item)
        {
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Item Item)
        {
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
        }

        public async Task<Item> FindByIdAsync(int? id)
        {
            return await _context.Items
                .FirstOrDefaultAsync(x => x.ItemID == id);
        }

        public async Task<List<Item>> GetListAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<SelectList> GetSelectListAsync(int? selectedItemId)
        {
            var categoryList = await _context.Items
                 .Select(x => new { x.ItemID, x.Name })
                 .OrderBy(x => x.Name)
                 .ToListAsync();
            return new SelectList(categoryList, "CategoryID", "Name", selectedItemId);
        }
        public bool IsExists(int id)
        {
            return _context.Items.Any(e => e.ItemID == id);
        }

        public async Task UpdateAsync(Item Item)
        {
            _context.Items.Update(Item);
            await _context.SaveChangesAsync();
        }
    }
}
