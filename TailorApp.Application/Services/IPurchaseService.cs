﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Domain.Entities.PurchaseModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IPurchaseService : IScopedService
    {
        Task<List<Purchase>> GetListAsync();
        bool DetailIsExists(int id);
        Task<Purchase> FindByIdAsync(int? id);
        Task CreateAsync(Purchase purchase);
        Task UpdateAsync(Purchase purchase);
        Task<PurchaseDetail> FindDetailByIdAsync(int? id);
        Task UpdateDetailAsync(PurchaseDetail purchaseDetail);
        Task<object> GetDataTableAsync(DataTableDto dataTableDto);
    }
}
