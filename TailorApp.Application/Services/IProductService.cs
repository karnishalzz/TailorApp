using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IProductService : IScopedService
    {
        Task<List<Product>> GetListAsync();
        bool IsExists(int id);
        Task<Product> FindByIdAsync(int? id);
        Task CreateAsync(Product Product);
        Task UpdateAsync(Product Product);
        Task DeleteAsync(Product product);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
