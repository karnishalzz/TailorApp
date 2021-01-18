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
    public class RentReturnService : IRentReturnService
    {
        private readonly IRentReturnRepository _rentReturnRepository;
        public RentReturnService(IRentReturnRepository rentReturnRepository)
        {
            _rentReturnRepository=rentReturnRepository;
        }

        public async Task CreateAsync(RentReturn rentReturn) => await _rentReturnRepository.CreateAsync(rentReturn);
        public async Task<RentReturn> FindByIdAsync(int id) =>await _rentReturnRepository.FindByIdAsync(id);
        public async Task<List<RentReturn>> GetListAsync() => await _rentReturnRepository.GetListAsync();
        
    }
}
