using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Infrastructure.Data.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly ApplicationDbContext _context;
        public MeasurementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Measurement> Measurements => _context.Measurements.AsQueryable();

        public async Task<List<Measurement>> GetListAsync()
        {
            return await _context.Measurements.AsNoTracking().ToListAsync();
        }
        public bool IsExists(int id)
        {
            return _context.Measurements.Any(e => e.MeasurementID == id);
        }
        public async Task<Measurement> FindByIdAsync(int? id)
        {
            return await _context.Measurements.FindAsync(id);
        }

        public async Task CreateAsync(Measurement measurement)
        {
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Measurement measurement)
        {
            _context.Measurements.Update(measurement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Measurement item = await FindByIdAsync(id);
            _context.Measurements.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
