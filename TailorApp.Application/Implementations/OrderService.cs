using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task CreateAsync(Order Order)
        {
            await _orderRepository.CreateAsync(Order);
        }

        public async Task DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<Order> FindByIdAsync(int? id)
        {
            return await _orderRepository.FindByIdAsync(id);
        }

        public async Task<OrderDetailMeasurement> GetDetailMeasurementById(int oderdetailId, int measurementId)
        {
            return await _orderRepository.GetDetailMeasurementById(oderdetailId, measurementId);
        }

        public async Task<List<OrderDetail>> GetDetailsById(int orderId)
        {
           return await _orderRepository.GetDetailsById(orderId);
        }

        public async Task<List<Order>> GetListAsync()
        {
            return await _orderRepository.GetListAsync();
        }

        public bool IsExists(int id)
        {
            return _orderRepository.IsExists(id);
        }

        public async Task UpdateAsync(Order Order)
        {
            await _orderRepository.UpdateAsync(Order);
        }

        public async Task UpdateDetailMeasurementAsync(OrderDetailMeasurement item)
        {
            await _orderRepository.UpdateDetailMeasurementAsync(item);
        }
    }
}
