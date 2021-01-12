using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface ICustomerRepository : IScopedService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCustomerId);

    }
}
