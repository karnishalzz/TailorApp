using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IMeasurementRepository : IScopedService
    {
        IQueryable<Measurement> Measurements { get; }
        Task<List<Measurement>> GetListAsync();
        bool IsExists(int id);
        Task<Measurement> GetByIdAsync(int? id);
        Task<Measurement> FindByIdAsync(int? id);
        Task CreateAsync(Measurement measurement);
        Task UpdateAsync(Measurement measurement);
        Task DeleteAsync(int id);


    }
}
