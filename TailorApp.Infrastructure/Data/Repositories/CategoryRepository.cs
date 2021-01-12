using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<SelectList> GetSelectListAsync(int? selectedCategoryId)
        {
            var categoryList =await _context.Categories
                 .Select(x => new { x.CategoryID, x.Name })
                 .OrderBy(x => x.Name)
                 .ToListAsync();
            return new SelectList(categoryList, "CategoryID", "Name", selectedCategoryId);
        }
    }
}
