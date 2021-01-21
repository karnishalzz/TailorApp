using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateAsync(Product Product)=> await _productRepository.CreateAsync(Product);
        public async Task DeleteAsync(Product product)=> await _productRepository.DeleteAsync(product);
        public async Task<Product> FindByIdAsync(int? id)=> await _productRepository.FindByIdAsync(id);
        public async Task<List<Product>> GetListAsync()=> await _productRepository.GetListAsync();
        public bool IsExists(int id)=> _productRepository.IsExists(id);
        public async Task UpdateAsync(Product Product) => await _productRepository.UpdateAsync(Product);
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
                string category = dataTableDto.Columns[3].Search.Value;
               

                IQueryable<Product> productAsQueryable = _productRepository.Products;

                int recordsTotal = productAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    productAsQueryable = productAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    productAsQueryable = productAsQueryable.Where(m => m.Description.Contains(description));
                }
                if (!string.IsNullOrWhiteSpace(category))
                {
                    productAsQueryable = productAsQueryable.Where(m => m.Category.Name.Contains(category));
                }


                int recordsFiltered = productAsQueryable.Count();

                var products = await productAsQueryable.Select(m => new
                {
                    m.ProductID,
                    m.Name,
                    m.Description,
                    Category=m.Category.Name,
                    m.ImagePath
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = products
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
