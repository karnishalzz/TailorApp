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
        
    }
}
