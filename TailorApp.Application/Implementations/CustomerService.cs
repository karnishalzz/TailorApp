using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Application.Services;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<SelectList> GetSelectListAsync(int? selectedCustomerId = null)
        {
            return await _customerRepository.GetSelectListAsync(selectedCustomerId);
        }
    }
}
