using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories
            .Include(x=>x.Enrollments)
            .ThenInclude(y=>y.Measurement)
            .AsQueryable();

        public async Task<List<Category>> GetListAsync()
        {
            return await _context.Categories
                .Include(i => i.Enrollments)
                 .ThenInclude(i => i.Measurement)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<SelectList> GetSelectListAsync(int? selectedCategoryId)
        {
            var categoryList = await _context.Categories
                 .Select(x => new { x.CategoryID, x.Name })
                 .OrderBy(x => x.Name)
                 .ToListAsync();
            return new SelectList(categoryList, "CategoryID", "Name", selectedCategoryId);
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.AddRange(category);
            await _context.SaveChangesAsync();
        }
        public async Task<Category> FindByIdAsync(int? id)
        {
            return await _context.Categories
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x=>x.CategoryID==id);


        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.UpdateRange(category);
            await _context.SaveChangesAsync();
        }
        public bool IsExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
        public async Task DeleteAsync(int id)
        {
            Category item = await FindByIdAsync(id);
            _context.Categories.RemoveRange(item);
            await _context.SaveChangesAsync();
        }

        public object GetMeasurementsByCategoryId(int id)
        {
            var item=
                  from c in _context.Categories
                  join e in _context.Enrollments on c.CategoryID equals e.CategoryID
                  join m in _context.Measurements on e.MeasurementID equals m.MeasurementID
                  where c.CategoryID == id
                  select new
                  {
                      m.MeasurementID,
                      m.Name,
                  };
            return item;


        }
    }
}
