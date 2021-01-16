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


        public async Task<List<Stock>> GetListAsync()
        {
            return await _stockRepository.GetListAsync();
        }
        
        public async Task<Stock> FindByIdAsync(int? id)
        {
            return await _stockRepository.FindByIdAsync(id);
        }


    }
}
