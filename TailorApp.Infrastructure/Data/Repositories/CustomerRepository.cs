using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<Customer> Customers => _context.Customers.AsQueryable();

        public async Task CreateAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> FindByIdAsync(int? id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(x => x.CustomerID == id);
        }

        public async Task<List<Customer>> GetListAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync();
        }

        public bool IsExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        public async Task UpdateAsync(Customer Customer)
        {
            _context.Customers.Update(Customer);
            await _context.SaveChangesAsync();
        }
        public async Task<SelectList> GetSelectListAsync(int? selectedCustomerId)
        {
            var customerList = await _context.Customers.Select(c => new { c.CustomerID, c.Name })
                .OrderBy(c => c.Name).ToListAsync();

            return new SelectList(customerList, "CustomerID", "Name", selectedCustomerId);
        }

       
    }
}
