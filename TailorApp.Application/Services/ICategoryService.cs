using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ICategoryService : IScopedService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCategoryId = null);
    }
}
