using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities.RentModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;

        public RentService(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }
        public int Total => _rentRepository.Total;
        public int Monthly => _rentRepository.Monthly;
        public async Task CreateAsync(Rent rent) => await _rentRepository.CreateAsync(rent);
        public async Task UpdateAsync(Rent rent) => await _rentRepository.UpdateAsync(rent);
        public async Task<Rent> FindByIdAsync(int? id) => await _rentRepository.FindByIdAsync(id);
        public async Task<RentDetail> FindDetailByIdAsync(int id) => await _rentRepository.FindDetailByIdAsync(id);
        public async Task<List<Rent>> GetListAsync() => await _rentRepository.GetListAsync();

        
    }
}
