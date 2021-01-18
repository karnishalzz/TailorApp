using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.SalesModule;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ISaleService : IScopedService
    {
        Task<List<Sales>> GetListAsync();
        Task<Sales> FindByIdAsync(int? id);
        Task CreateAsync(Sales sale);
    }
}
