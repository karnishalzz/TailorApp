using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
                string price = dataTableDto.Columns[2].Search.Value;
                string description = dataTableDto.Columns[3].Search.Value;
                string date = dataTableDto.Columns[4].Search.Value;
                decimal _price;
                DateTime time;

                IQueryable<Income> expenseAsQueryable = _incomeRepository.Incomes;

                int recordsTotal = expenseAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(price) && decimal.TryParse(price,out _price))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Price==_price);
                }
                if (!string.IsNullOrWhiteSpace(description))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Description.Contains(description));
                }

                if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date,out time))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Date==time);
                }


                int recordsFiltered = expenseAsQueryable.Count();

                var incomes = await expenseAsQueryable.Select(m => new
                {
                    m.IncomeID,
                    m.Name,
                    m.Price,
                    m.Description,
                    Date = m.Date.ToShortDateString()
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = incomes
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
