using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface ICustomerRepository : IScopedService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCustomerId);
        Task<List<Customer>> GetListAsync();
        bool IsExists(int id);
        Task<Customer> FindByIdAsync(int? id);
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);

    }
}
