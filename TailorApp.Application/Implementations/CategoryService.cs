using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Services;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class CategoryService : ICategoryService
    {

        private readonly ICustomerRepository _customerRepository;
        public CategoryService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<SelectList> GetSelectListAsync(int? selectedCategoryId)
        {
            return await _customerRepository.GetSelectListAsync(selectedCategoryId);
        }
    }
}
