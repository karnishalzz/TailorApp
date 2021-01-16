﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IIncomeService : IScopedService
    {
        Task<List<Income>> GetListAsync();
        Task<Income> FindByIdAsync(int id);
        Task<Income> GetByOrderId(int orderId);
        Task UpdateAsync(Income income);
        Task CreateAsync(Income income);
    }
}
