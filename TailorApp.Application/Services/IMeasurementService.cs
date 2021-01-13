using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IMeasurementService : IScopedService
    {
        Task<List<Measurement>> GetListAsync();
        bool IsExists(int id);
        Task<Measurement> GetByIdAsync(int? id);
        Task<Measurement> FindByIdAsync(int? id);
        Task CreateAsync(Measurement measurement);
        Task UpdateAsync(Measurement measurement);
        Task DeleteAsync(int id);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
