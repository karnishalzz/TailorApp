﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.InventoryModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IItemService : IScopedService
    {
        Task<List<Item>> GetListAsync();
        bool IsExists(int id);
        Task<Item> FindByIdAsync(int? id);
        Task CreateAsync(Item Item);
        Task UpdateAsync(Item Item);
        Task DeleteAsync(Item Item);
    }
}
