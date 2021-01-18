using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class StockService: IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }


        public async Task<List<Stock>> GetListAsync()=>await _stockRepository.GetListAsync();


        public async Task<Stock> FindByIdAsync(int? id)=> await _stockRepository.FindByIdAsync(id);
        

        public async Task<List<Stock>> GetByItemCategory(int itemId, CategoryType category) =>
            await _stockRepository.GetByItemCategory(itemId, category);

        public async Task CreateAsync(Stock stock) => await _stockRepository.CreateAsync(stock);

        public async Task UpdateStockListAsync(List<Stock> stocks) => await _stockRepository.UpdateStockListAsync(stocks);

        public async Task<List<Stock>> GetListByCategoryAsync(CategoryType categoryType) => 
            await _stockRepository.GetListByCategoryAsync(categoryType);
       
    }
}
