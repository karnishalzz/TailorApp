﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Application.Services
{
    public interface IOrderService : IScopedService
    {
        Task<List<Order>> GetListAsync();
        bool IsExists(int id);
        Task<Order> FindByIdAsync(int? id);
        Task<List<OrderDetail>> GetDetailsById(int orderId);
        Task<OrderDetailMeasurement> GetDetailMeasurementById(int oderdetailId, int measurementId);
        Task UpdateDetailMeasurementAsync(OrderDetailMeasurement item);
        Task CreateAsync(Order Order);
        Task UpdateAsync(Order Order);
        Task DeleteAsync(int id);
    }
}