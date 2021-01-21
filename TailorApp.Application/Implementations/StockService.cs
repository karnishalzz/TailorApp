using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
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
                string item = dataTableDto.Columns[1].Search.Value;
                string category = dataTableDto.Columns[2].Search.Value;
                string initialQuantity = dataTableDto.Columns[3].Search.Value;
                string quantity = dataTableDto.Columns[4].Search.Value;
                string costPrice = dataTableDto.Columns[5].Search.Value;
                string sellingPrice = dataTableDto.Columns[6].Search.Value;
                string date = dataTableDto.Columns[7].Search.Value;
                decimal selling, cost;
                int iniqty, qty;
                DateTime time;

                IQueryable<Stock> stockAsQueryable = _stockRepository.Stocks;

                int recordsTotal = stockAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(item))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.Item.Name.Contains(item));
                }

                if (!string.IsNullOrWhiteSpace(category))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.Category.ToString().Contains(category));
                }
                if (!string.IsNullOrWhiteSpace(initialQuantity) && int.TryParse(initialQuantity, out iniqty))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.InitialQuantity==int.Parse(initialQuantity));
                }

                if (!string.IsNullOrWhiteSpace(quantity) && int.TryParse(quantity, out qty))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.Quantity == qty);
                }
                if (!string.IsNullOrWhiteSpace(costPrice) && decimal.TryParse(costPrice, out cost))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.CostPrice == cost);
                }

                if (!string.IsNullOrWhiteSpace(sellingPrice) && decimal.TryParse(sellingPrice,out selling))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.SellingPrice == selling);
                }
                if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date,out time))
                {
                    stockAsQueryable = stockAsQueryable.Where(m => m.Purchase.Date == time);
                }


                int recordsFiltered = stockAsQueryable.Count();


                var stocks = await stockAsQueryable.Select(m => new
                {
                    m.StockID,
                    Item=m.Item.Name,
                    Category=m.Category.ToString(),
                    m.InitialQuantity,
                    m.Quantity,
                    m.CostPrice,
                    m.SellingPrice,
                    m.Purchase.Date,
                  
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = stocks
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
