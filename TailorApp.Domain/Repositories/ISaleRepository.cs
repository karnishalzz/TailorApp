using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.SalesModule;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface ISaleRepository : IScopedService
    {
        int Total { get; }
        int Monthly { get; }
        IQueryable<Sales> Sales { get; }
        Task<List<Sales>> GetListAsync();
        Task<Sales> FindByIdAsync(int? id);
        Task CreateAsync(Sales sale);
        object GetByYearAndMonth(int year, int month);
        Task<List<Sales>> GetByDateAsync(DateTime date);
        object GetByYear(int year);
    }
}
