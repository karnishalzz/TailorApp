using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ICategoryService : IScopedService
    {
        Task<List<Category>> GetListAsync();
        Task<SelectList> GetSelectListAsync(int? selectedCategoryId = null);
        object GetMeasurementsByCategoryId(int id);
        bool IsExists(int id);
        Task<Category> FindByIdAsync(int? id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
