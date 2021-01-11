using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SelectList> GetSelectListAsync(int? selectedCustomerId)
        {
            var customerList = await _dbContext.Customers.Select(c => new { c.CustomerID, c.Name })
                .OrderBy(c => c.Name).ToListAsync();

            return new SelectList(customerList, "CustomerID", "Name", selectedCustomerId);
        }
    }
}
