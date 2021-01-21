using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities.PurchaseModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ISupplierService: IScopedService
    {
        Task<List<Supplier>> GetListAsync();
        Task<SelectList> GetSelectListAsync(int? selectedSupplierId=null);
        bool IsExists(int id);
        Task<Supplier> FindByIdAsync(int? id);
        Task CreateAsync(Supplier Supplier);
        Task UpdateAsync(Supplier Supplier);
        Task DeleteAsync(int id);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
