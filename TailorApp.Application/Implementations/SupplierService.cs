using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities.PurchaseModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class SupplierService: ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task CreateAsync(Supplier Supplier)
        {
            await _supplierRepository.CreateAsync(Supplier);
        }

        public async Task DeleteAsync(int id )
        {
            await _supplierRepository.DeleteAsync(id);
        }

        public async Task<Supplier> FindByIdAsync(int? id)
        {
            return await _supplierRepository.FindByIdAsync(id);
        }

        public async Task<List<Supplier>> GetListAsync()
        {
            return await _supplierRepository.GetListAsync();
        }

        public bool IsExists(int id)
        {
            return _supplierRepository.IsExists(id);
        }

        public async Task UpdateAsync(Supplier Supplier)
        {
            await _supplierRepository.UpdateAsync(Supplier);
        }
    }
}
