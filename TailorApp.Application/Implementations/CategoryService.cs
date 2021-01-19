using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
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

        public async Task<object> GetDataTableAsync(DataTableDto dataTableDto)
        {
            try
            {
                if (dataTableDto == null)
                {
                    throw new ArgumentNullException(nameof(dataTableDto));
                }

                int draw = dataTableDto.Draw;
                int start = dataTableDto.Start;
                int length = dataTableDto.Length;

                // Sorting Column and order
                string sortColumnName = dataTableDto.Columns[dataTableDto.Order[0].Column].Name;
                string sortColumnDir = dataTableDto.Order[0].Dir;

                // Individual Column Search value
                string name = dataTableDto.Columns[1].Search.Value;
                string description = dataTableDto.Columns[2].Search.Value;

                IQueryable<Category> categoryAsQueryable = _categoryRepository.Categories;

                int recordsTotal = categoryAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    categoryAsQueryable = categoryAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    categoryAsQueryable = categoryAsQueryable.Where(m => m.Description.Contains(description));
                }


                int recordsFiltered = categoryAsQueryable.Count();

                var categories = await categoryAsQueryable.Select(m => new
                {
                    m.CategoryID,
                    m.Name,
                    
                    m.Description
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = categories
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
