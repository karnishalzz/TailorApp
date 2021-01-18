using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.RentModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{

    public interface IRentRepository : IScopedService
    {
        IQueryable<Rent> Rents { get; }
        Task<List<Rent>> GetListAsync();
        Task CreateAsync(Rent rent);
        Task UpdateAsync(Rent rent);
        Task<Rent> FindByIdAsync(int? id);
        Task<RentDetail> FindDetailByIdAsync(int id);
    }
}
