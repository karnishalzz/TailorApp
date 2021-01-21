using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IStaffRepository : IScopedService
    {
        IQueryable<Staff> Staffs { get; }
        Task<List<Staff>> GetListAsync();
        bool IsExists(int id);
        Task<Staff> FindByIdAsync(int? id);
        Task CreateAsync(Staff staff);
        Task UpdateAsync(Staff staff);
        Task DeleteAsync(Staff staff);
    }
}
