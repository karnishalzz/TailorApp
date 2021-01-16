﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.PurchaseModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface ISupplierService: IScopedService
    {
        Task<List<Supplier>> GetListAsync();
        bool IsExists(int id);
        Task<Supplier> FindByIdAsync(int? id);
        Task CreateAsync(Supplier Supplier);
        Task UpdateAsync(Supplier Supplier);
        Task DeleteAsync(int id);
    }
}