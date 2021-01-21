using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.RentModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IRentReturnRepository : IScopedService
    {
        IQueryable<RentReturn> RentReturns{ get; }
        Task<List<RentReturn>> GetListAsync();
        Task<RentReturn> FindByIdAsync(int id);
        Task CreateAsync(RentReturn rentReturn);
       
    }
}
