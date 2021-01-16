using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IExpenseService : IScopedService
    {
        Task<List<Expense>> GetListAsync();
        bool IsExists(int id);
        Task<Expense> FindByIdAsync(int? id);
        Task CreateAsync(Expense Expense);
        Task UpdateAsync(Expense Expense);
        Task DeleteAsync(int id);
    }
}
