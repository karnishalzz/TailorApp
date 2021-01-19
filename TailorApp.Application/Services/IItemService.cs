using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities.InventoryModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IItemService : IScopedService
    {
        Task<List<Item>> GetListAsync();

        Task<SelectList> GetSelectListAsync(int? selectedItemId = null);
        bool IsExists(int id);
        Task<Item> FindByIdAsync(int? id);
        Task CreateAsync(Item Item);
        Task UpdateAsync(Item Item);
        Task DeleteAsync(Item Item);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
