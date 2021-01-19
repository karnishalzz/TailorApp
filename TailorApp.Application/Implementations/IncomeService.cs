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
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        public decimal Total => _incomeRepository.Total;
        public decimal Monthly => _incomeRepository.Monthly;
        public async Task CreateAsync(Income income) =>await _incomeRepository.CreateAsync(income);

        public async Task<Income> FindByIdAsync(int id) => await _incomeRepository.FindByIdAsync(id);
        

        public async Task<Income> GetByOrderId(int orderId)=> await _incomeRepository.GetByOrderId(orderId);

        public async Task<Income> GetByRentId(int rentId) =>await _incomeRepository.GetByRentId(rentId);
        public async Task<List<Income>> GetListAsync()=> await _incomeRepository.GetListAsync();
        
        public async Task UpdateAsync(Income income)=>await _incomeRepository.UpdateAsync(income);

    }
}
