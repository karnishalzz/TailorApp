using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
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

        public async Task<PurchaseDetail> FindDetailByIdAsync(int? id) =>await _purchaseRepository.FindDetailByIdAsync(id);

        public async Task UpdateDetailAsync(PurchaseDetail purchaseDetail) =>await _purchaseRepository.UpdateDetailAsync(purchaseDetail);

        public async Task CreateAsync(Purchase purchase) => await _purchaseRepository.CreateAsync(purchase);
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
                string date = dataTableDto.Columns[1].Search.Value;
                string amount = dataTableDto.Columns[2].Search.Value;
                string discount = dataTableDto.Columns[3].Search.Value;
                string tax = dataTableDto.Columns[4].Search.Value;
                string grandTotal = dataTableDto.Columns[5].Search.Value;
                string description = dataTableDto.Columns[6].Search.Value;
                string supplier = dataTableDto.Columns[7].Search.Value;

                decimal _amount,_total,_tax,_discount;
                DateTime time;

                IQueryable<Purchase> purchaseAsQueryable = _purchaseRepository.Purchases;

                int recordsTotal = purchaseAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date,out time))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.Date==time);
                }

                if (!string.IsNullOrWhiteSpace(amount) && decimal.TryParse(amount, out _amount))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.Amount == _amount);
                }
                if (!string.IsNullOrWhiteSpace(discount) && decimal.TryParse(discount, out _discount))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.Discount == _discount);
                }
                if (!string.IsNullOrWhiteSpace(tax) && decimal.TryParse(tax, out _tax))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.Tax == _tax);
                }
                if (!string.IsNullOrWhiteSpace(grandTotal) && decimal.TryParse(grandTotal, out _total))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.GrandTotal == _total);
                }
                if (!string.IsNullOrWhiteSpace(description))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.Description.Contains(description));
                }

                if (!string.IsNullOrWhiteSpace(supplier))
                {
                    purchaseAsQueryable = purchaseAsQueryable.Where(m => m.Supplier.Name == supplier);
                }


                int recordsFiltered = purchaseAsQueryable.Count();

                var purchases = await purchaseAsQueryable.Select(m => new
                {
                    m.PurchaseID,
                    Date=m.Date.ToShortDateString(),
                    m.Amount,
                    m.Discount,
                    m.Tax,
                    m.GrandTotal,
                    m.Description,
                    Supplier = m.Supplier.Name
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = purchases
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
