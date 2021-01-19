using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IIncomeRepository : IScopedService
    {
        IQueryable<Income> Incomes { get; }
        decimal Total { get; }
        decimal Monthly { get; }
        Task<List<Income>> GetListAsync();
        Task<Income> FindByIdAsync(int id);
        Task<Income> GetByOrderId(int orderId);
        Task<Income> GetByRentId(int rentId);
        Task UpdateAsync(Income income);
        Task CreateAsync(Income income );

    }
}
