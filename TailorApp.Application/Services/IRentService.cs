using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities.RentModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IRentService: IScopedService
    {
        int Total { get; }
        int Monthly { get; }
        Task CreateAsync(Rent rent);
        Task UpdateAsync(Rent rent);
        Task<List<Rent>> GetListAsync();
        Task<Rent> FindByIdAsync(int? id);
        Task<RentDetail> FindDetailByIdAsync(int id);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
