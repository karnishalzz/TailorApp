using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ICustomerService : IScopedService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCustomerId = null);
        Task<List<Customer>> GetListAsync();
        bool IsExists(int id);
        Task<Customer> FindByIdAsync(int? id);
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}
