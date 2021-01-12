using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Application.Services;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository customerRepository)
        {
            _categoryRepository = customerRepository;
        }
        public async Task<SelectList> GetSelectListAsync(int? selectedCategoryId = null)
        {
            return await _categoryRepository.GetSelectListAsync(selectedCategoryId);
        }
    }
}
