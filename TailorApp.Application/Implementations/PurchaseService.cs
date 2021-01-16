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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Purchase> FindByIdAsync(int? id) => await _purchaseRepository.FindByIdAsync(id);

        
        public async Task<List<Purchase>> GetListAsync() => await _purchaseRepository.GetListAsync();

        public bool DetailIsExists(int id) => _purchaseRepository.DetailIsExists(id);

        public async Task UpdateAsync(Purchase purchase) =>await _purchaseRepository.UpdateAsync(purchase);

        public async Task<PurchaseDetail> FindDetailByIdAsync(int id) =>await _purchaseRepository.FindDetailByIdAsync(id);

        public async Task UpdateDetailAsync(PurchaseDetail purchaseDetail) =>await _purchaseRepository.UpdateDetailAsync(purchaseDetail);
    }
}
