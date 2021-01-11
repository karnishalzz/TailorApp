using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TailorApp.Application.Services
{
    public interface ICustomerService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCustomerId);
    }
}
