using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task CreateAsync(Expense Expense)=> await _expenseRepository.CreateAsync(Expense);
       
        public async Task DeleteAsync(int id)=> await _expenseRepository.DeleteAsync(id);
       
        public async Task<Expense> FindByIdAsync(int? id)=> await _expenseRepository.FindByIdAsync(id);
       
        public async Task<List<Expense>> GetListAsync()=> await _expenseRepository.GetListAsync();
      
        public bool IsExists(int id)=> _expenseRepository.IsExists(id);
        public async Task UpdateAsync(Expense Expense)=> await _expenseRepository.UpdateAsync(Expense);
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
                string type = dataTableDto.Columns[1].Search.Value;
                string price = dataTableDto.Columns[2].Search.Value;
                string description = dataTableDto.Columns[3].Search.Value;
                string date = dataTableDto.Columns[4].Search.Value;
                decimal _price;
                DateTime time;

                IQueryable<Expense> expenseAsQueryable = _expenseRepository.Expenses;

                int recordsTotal = expenseAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(type))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Type.ToString().Contains(type));
                }

                if (!string.IsNullOrWhiteSpace(price) && decimal.TryParse(price,out _price))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Price==_price);
                }
                if (!string.IsNullOrWhiteSpace(description))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Description.Contains(description));
                }

                if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out time))
                {
                    expenseAsQueryable = expenseAsQueryable.Where(m => m.Date==time);
                }


                int recordsFiltered = expenseAsQueryable.Count();

                var expenses = await expenseAsQueryable.Select(m => new
                {
                    m.ExpenseID,
                    Type=m.Type.ToString(),
                    m.Price,
                    m.Description,
                    Date=m.Date.ToShortDateString(),
                   
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = expenses
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
