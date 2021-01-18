using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
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

        public async Task CreateAsync(Customer customer)=> await _customerRepository.CreateAsync(customer);
        
        public async Task DeleteAsync(Customer customer)=> await _customerRepository.DeleteAsync(customer);

        public async Task<Customer> FindByIdAsync(int? id)=> await _customerRepository.FindByIdAsync(id);

        public async Task<List<Customer>> GetListAsync()=> await _customerRepository.GetListAsync();
        public bool IsExists(int id)=> _customerRepository.IsExists(id);
      
        public async Task UpdateAsync(Customer Customer)=> await _customerRepository.UpdateAsync(Customer);
        public async Task<SelectList> GetSelectListAsync(int? selectedCustomerId = null)=>
            await _customerRepository.GetSelectListAsync(selectedCustomerId);

    }
}
