using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IOrderRepository : IScopedService
    {
        IQueryable<Order> Orders { get; }
        int Total { get; }
        int TotalDelivered { get; }
        Task<List<Order>> GetListAsync();
        bool IsExists(int id);
        Task<Order> FindByIdAsync(int? id);
        Task<List<OrderDetail>> GetDetailsById(int orderId);
        Task<OrderDetailMeasurement> GetDetailMeasurementById(int oderdertailId,int measurementId);
        Task UpdateDetailMeasurementAsync(OrderDetailMeasurement item);
        Task CreateAsync(Order Order);
        Task CreateDetailWithMeasurementAsync(OrderDetail orderDetail);
        Task UpdateAsync(Order Order);
        Task DeleteAsync(int id);
    
    }
}
