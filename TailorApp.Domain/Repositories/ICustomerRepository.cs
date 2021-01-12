using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TailorApp.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<SelectList> GetSelectListAsync(int? selectedCustomerId);
       
    }
}
