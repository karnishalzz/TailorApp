using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.RentModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IRentService: IScopedService
    {
        Task CreateAsync(Rent rent);
        Task UpdateAsync(Rent rent);
        Task<List<Rent>> GetListAsync();
        Task<Rent> FindByIdAsync(int? id);
        Task<RentDetail> FindDetailByIdAsync(int id);
        
    }
}
