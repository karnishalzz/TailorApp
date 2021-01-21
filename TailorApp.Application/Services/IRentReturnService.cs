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
    public interface IRentReturnService : IScopedService
    {
        Task<List<RentReturn>> GetListAsync();
        Task<RentReturn> FindByIdAsync(int id);
        Task CreateAsync(RentReturn rentReturn);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);

    }
}
