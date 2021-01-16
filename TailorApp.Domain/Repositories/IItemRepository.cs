using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.InventoryModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IItemRepository : IScopedService
    {
        IQueryable<Item> Items { get; }
        Task<List<Item>> GetListAsync();
        Task<SelectList> GetSelectListAsync(int? selectedItemId);
        bool IsExists(int id);
        Task<Item> FindByIdAsync(int? id);
        Task CreateAsync(Item Item);
        Task UpdateAsync(Item Item);
        Task DeleteAsync(Item Item);
    }
}
