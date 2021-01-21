using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
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

        public async Task<object> GetDataTableAsync(DataTableDto dataTableDto)
        {
            try
            {
                if (dataTableDto == null)
                {
                    throw new ArgumentNullException(nameof(dataTableDto));
                }

                int draw = dataTableDto.Draw;
                int start = dataTableDto.Start;
                int length = dataTableDto.Length;

                // Sorting Column and order
                string sortColumnName = dataTableDto.Columns[dataTableDto.Order[0].Column].Name;
                string sortColumnDir = dataTableDto.Order[0].Dir;

                // Individual Column Search value
                string deliverDate = dataTableDto.Columns[1].Search.Value;
                string orderDate = dataTableDto.Columns[2].Search.Value;
                string customer = dataTableDto.Columns[3].Search.Value;
                DateTime _orderDate,_deliverDate;
             

                IQueryable<Order> orderAsQueryable = _orderRepository.Orders;

                int recordsTotal = orderAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(deliverDate) && DateTime.TryParse(deliverDate,out _deliverDate))
                {
                    orderAsQueryable = orderAsQueryable.Where(m => m.DeliverDate==_deliverDate);
                }
                if (!string.IsNullOrWhiteSpace(orderDate) && DateTime.TryParse(orderDate, out _orderDate))
                {
                    orderAsQueryable = orderAsQueryable.Where(m => m.DeliverDate == _orderDate);
                }

                if (!string.IsNullOrWhiteSpace(customer))
                {
                    orderAsQueryable = orderAsQueryable.Where(m => m.Customer.Name.Contains(customer));
                }
               

                int recordsFiltered = orderAsQueryable.Count();


                var orders = await orderAsQueryable.Select(m => new
                {
                    m.OrderID,
                    DeliverDate=m.DeliverDate.ToShortDateString(),
                    OrderDate=m.OrderDate.ToShortDateString(),
                    Customer=m.Customer.Name,
                    m.IsDelivered,
                   
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = orders
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
