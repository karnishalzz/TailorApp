using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(Category category)=> await _categoryRepository.CreateAsync(category);
       
        public async Task DeleteAsync(int id)=> await _categoryRepository.DeleteAsync(id);
       
        public async Task<Category> FindByIdAsync(int? id)=> await _categoryRepository.FindByIdAsync(id);
      
        public async Task<List<Category>> GetListAsync()=> await _categoryRepository.GetListAsync();

        public object GetMeasurementsByCategoryId(int id) => _categoryRepository.GetMeasurementsByCategoryId(id);
        public async Task<SelectList> GetSelectListAsync(int? selectedCategoryId = null)=> await _categoryRepository.GetSelectListAsync(selectedCategoryId);

        public bool IsExists(int id)=> _categoryRepository.IsExists(id);
        public async Task UpdateAsync(Category category) => await _categoryRepository.UpdateAsync(category);
    }
}
