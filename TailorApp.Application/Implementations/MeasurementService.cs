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
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;

        public MeasurementService(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }


        public async Task<List<Measurement>> GetListAsync()
        {
            return await _measurementRepository.GetListAsync();
        }
        public bool IsExists(int id)
        {
            return _measurementRepository.IsExists(id);
        }
        public async Task<Measurement> GetByIdAsync(int? id)
        {
            return await _measurementRepository.GetByIdAsync(id);
        }

        public async Task<Measurement> FindByIdAsync(int? id)
        {
            return await _measurementRepository.FindByIdAsync(id);
        }

        public async Task CreateAsync(Measurement measurement)
        {
            await _measurementRepository.CreateAsync(measurement);
        }

        public async Task UpdateAsync(Measurement measurement)
        {
            await _measurementRepository.UpdateAsync(measurement);
        }

        public async Task DeleteAsync(int id) 
        {
            await _measurementRepository.DeleteAsync(id);
        }
    }
}
