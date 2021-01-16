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
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task CreateAsync(Staff Staff)
        {
            await _staffRepository.CreateAsync(Staff);
        }

        public async Task DeleteAsync(Staff Staff)
        {
            await _staffRepository.DeleteAsync(Staff);
        }

        public async Task<Staff> FindByIdAsync(int? id)
        {
            return await _staffRepository.FindByIdAsync(id);
        }

        public async Task<List<Staff>> GetListAsync()
        {
            return await _staffRepository.GetListAsync();
        }

        public bool IsExists(int id)
        {
            return _staffRepository.IsExists(id);
        }

        public async Task UpdateAsync(Staff Staff)
        {
            await _staffRepository.UpdateAsync(Staff);
        }
    }
}
