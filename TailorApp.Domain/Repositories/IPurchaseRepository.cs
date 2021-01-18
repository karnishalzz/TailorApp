using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.PurchaseModel;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace TailorApp.Domain.Repositories
{
    public interface IPurchaseRepository: IScopedService
    {
        IQueryable<Purchase> Purchases { get; }
        Task<List<Purchase>> GetListAsync();
        Task<Purchase> FindByIdAsync(int? id);
        Task CreateAsync(Purchase purchase);
        Task UpdateAsync(Purchase purchase);
        bool DetailIsExists(int id);
        Task<PurchaseDetail> FindDetailByIdAsync(int id);
        Task UpdateDetailAsync(PurchaseDetail purchaseDetail);
    }
}
