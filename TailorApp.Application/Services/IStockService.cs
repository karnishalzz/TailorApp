using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.InventoryModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IStockService : IScopedService
    {
        Task<List<Stock>> GetListAsync();
        Task<List<Stock>> GetListByCategoryAsync(CategoryType categoryType);
        Task<Stock> FindByIdAsync(int? id);
        Task CreateAsync(Stock stock);
        Task UpdateStockListAsync(List<Stock> stocks);
        Task<List<Stock>> GetByItemCategory(int itemId, CategoryType category);

    }
}
