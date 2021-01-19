using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
