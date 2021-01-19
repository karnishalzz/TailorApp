using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface ICategoryRepository : IScopedService
    {
        IQueryable<Category> Categories { get; }
        Task<List<Category>> GetListAsync();
        object GetMeasurementsByCategoryId(int id);
        Task<SelectList> GetSelectListAsync(int? selectedCategoryId);
        bool IsExists(int id);
        Task<Category> FindByIdAsync(int? id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
