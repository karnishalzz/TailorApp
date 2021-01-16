using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task CreateAsync(Product Product)
        {
            await _productRepository.CreateAsync(Product);
        }

        public async Task DeleteAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task<Product> FindByIdAsync(int? id)
        {
            return await _productRepository.FindByIdAsync(id);
        }

        public async Task<List<Product>> GetListAsync()
        {
            return await _productRepository.GetListAsync();
        }

        public bool IsExists(int id)
        {
            return _productRepository.IsExists(id);
        }

        public async Task UpdateAsync(Product Product)
        {
            await _productRepository.UpdateAsync(Product);
        }
    }
}
