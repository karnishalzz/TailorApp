using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders.AsQueryable();
        public int Total => _context.Orders.Count();
        public int TotalDelivered =>_context.Orders.Where(o => o.IsDelivered == true).Count();

        public async Task<List<Order>> GetListAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                 .AsNoTracking()
                 .ToListAsync();
        }
        public async Task CreateAsync(Order Order)
        {
            _context.Orders.AddRange(Order);
            await _context.SaveChangesAsync();
        }
        public async Task<Order> FindByIdAsync(int? id)
        {
            return await _context.Orders
                .Where(x => x.OrderID == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(Order Order)
        {
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();
        }
        public bool IsExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
        public async Task DeleteAsync(int id)
        {
            Order item = _context.Orders.Find(id);
            _context.Orders.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> GetDetailsById(int orderId)
        {
           return await _context.OrderDetails
                .Where(m => m.OrderID == orderId)
                .Include(p => p.Category)
                .Include(p => p.OrderDetailMeasurements)
                .ThenInclude(p => p.Measurement)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderDetailMeasurement> GetDetailMeasurementById(int oderdertailId, int measurementId)
        {
            return await _context.OrderDetalMeasurements
                .Where(x => x.OrderDetailID == oderdertailId && x.MeasurementID == measurementId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailMeasurementAsync(OrderDetailMeasurement item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task CreateDetailWithMeasurementAsync(OrderDetail orderDetail)
        {
            _context.AddRange(orderDetail);
            await _context.SaveChangesAsync();
        }
    }
}
