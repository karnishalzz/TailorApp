using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IExpenseRepository : IScopedService
    {
        IQueryable<Expense> Expenses { get; }
        Task<List<Expense>> GetListAsync();
        bool IsExists(int id);
        Task<Expense> FindByIdAsync(int? id);
        Task CreateAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(int id);
    }
}
