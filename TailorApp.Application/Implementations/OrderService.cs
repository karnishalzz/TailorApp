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
        public int Total => _orderRepository.Total;
        public int TotalDelivered => _orderRepository.TotalDelivered;
        public async Task CreateAsync(Order Order)=> await _orderRepository.CreateAsync(Order);

        public async Task CreateDetailWithMeasurementAsync(OrderDetail orderDetail) =>await _orderRepository.CreateDetailWithMeasurementAsync(orderDetail);
        public async Task DeleteAsync(int id)=> await _orderRepository.DeleteAsync(id);
       
        public async Task<Order> FindByIdAsync(int? id)=> await _orderRepository.FindByIdAsync(id);
        
        public async Task<OrderDetailMeasurement> GetDetailMeasurementById(int oderdetailId, int measurementId)=>
            await _orderRepository.GetDetailMeasurementById(oderdetailId, measurementId);
       
        public async Task<List<OrderDetail>> GetDetailsById(int orderId)=> await _orderRepository.GetDetailsById(orderId);
       
        public async Task<List<Order>> GetListAsync()=>await _orderRepository.GetListAsync();
       
        public bool IsExists(int id)=> _orderRepository.IsExists(id);
      
        public async Task UpdateAsync(Order Order)=> await _orderRepository.UpdateAsync(Order);
       
        public async Task UpdateDetailMeasurementAsync(OrderDetailMeasurement item)=> 
            await _orderRepository.UpdateDetailMeasurementAsync(item);
    
    }
}
