using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities.SalesModule;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public int Total => _saleRepository.Total;
        public int Monthly => _saleRepository.Monthly;
        public async Task CreateAsync(Sales sale) => await _saleRepository.CreateAsync(sale);
        public async Task<Sales> FindByIdAsync(int? id) => await _saleRepository.FindByIdAsync(id);

        public object GetByYearAsync(int year) => _saleRepository.GetByYear(year);

        public object GetByYearAndMonth(int year, int month) => _saleRepository.GetByYearAndMonth(year, month);

        public async Task<List<Sales>> GetListAsync() => await _saleRepository.GetListAsync();

        public async Task<List<Sales>> GetByDateAsync(DateTime date) =>await _saleRepository.GetByDateAsync(date);

        public object GetByYear(int year) => _saleRepository.GetByYear(year);
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
                string date = dataTableDto.Columns[1].Search.Value;
                string amount = dataTableDto.Columns[2].Search.Value;
                string discount = dataTableDto.Columns[3].Search.Value;
                string tax = dataTableDto.Columns[4].Search.Value;
                string total = dataTableDto.Columns[5].Search.Value;
                string remarks = dataTableDto.Columns[6].Search.Value;
                decimal _amount,_discount,_tax,_total;
                DateTime time;

                IQueryable<Sales> saleAsQueryable = _saleRepository.Sales;

                int recordsTotal = saleAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date,out time))
                {
                    saleAsQueryable = saleAsQueryable.Where(m => m.Date==time);
                }

                if (!string.IsNullOrWhiteSpace(amount) && decimal.TryParse(amount, out _amount))
                {
                    saleAsQueryable = saleAsQueryable.Where(m => m.Amount == _amount);
                }
                if (!string.IsNullOrWhiteSpace(discount) && decimal.TryParse(discount, out _discount))
                {
                    saleAsQueryable = saleAsQueryable.Where(m => m.Discount == _discount);
                }
                if (!string.IsNullOrWhiteSpace(tax) && decimal.TryParse(tax, out _tax))
                {
                    saleAsQueryable = saleAsQueryable.Where(m => m.Tax == _tax);
                }
                if (!string.IsNullOrWhiteSpace(total) && decimal.TryParse(total, out _total))
                {
                    saleAsQueryable = saleAsQueryable.Where(m => m.GrandTotal == _total);
                }
                if (!string.IsNullOrWhiteSpace(remarks))
                {
                    saleAsQueryable = saleAsQueryable.Where(m => m.Remarks.Contains(remarks));
                }

                int recordsFiltered = saleAsQueryable.Count();

                var sales = await saleAsQueryable.Select(m => new
                {
                    m.SalesID,
                    Date=m.Date.ToShortDateString(),
                    m.Amount,
                    m.Discount,
                   m.Tax,
                   m.GrandTotal,
                   m.Remarks,

                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = sales
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
