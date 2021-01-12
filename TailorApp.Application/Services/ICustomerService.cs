using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ICustomerService : IScopedService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCustomerId = null);
    }
}
