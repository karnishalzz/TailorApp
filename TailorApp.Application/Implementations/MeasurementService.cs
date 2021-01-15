using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
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
                string name = dataTableDto.Columns[1].Search.Value;
                string description = dataTableDto.Columns[2].Search.Value;

                IQueryable<Measurement> measurementAsQueryable = _measurementRepository.Measurements;

                int recordsTotal = measurementAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    measurementAsQueryable = measurementAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    measurementAsQueryable = measurementAsQueryable.Where(m => m.Description.Contains(description));
                }


                int recordsFiltered = measurementAsQueryable.Count();

                var measurements = await measurementAsQueryable.Select(m => new
                {
                    m.MeasurementID,
                    m.Name,
                    m.Description
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = measurements
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
