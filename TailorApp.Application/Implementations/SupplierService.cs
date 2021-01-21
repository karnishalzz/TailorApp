using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class SupplierService: ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task CreateAsync(Supplier Supplier)=> await _supplierRepository.CreateAsync(Supplier);

        public async Task DeleteAsync(int id )=> await _supplierRepository.DeleteAsync(id);
       
        public async Task<Supplier> FindByIdAsync(int? id)=> await _supplierRepository.FindByIdAsync(id);
        public async Task<List<Supplier>> GetListAsync()=> await _supplierRepository.GetListAsync();
       
        public async Task<SelectList> GetSelectListAsync(int? selectedSupplierId)=> 
            await _supplierRepository.GetSelectListAsync(selectedSupplierId);
       
        public bool IsExists(int id)=> _supplierRepository.IsExists(id);
       
        public async Task UpdateAsync(Supplier Supplier)=> await _supplierRepository.UpdateAsync(Supplier);
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
                string contact = dataTableDto.Columns[2].Search.Value;
                string address = dataTableDto.Columns[3].Search.Value;
                string description = dataTableDto.Columns[4].Search.Value;

                IQueryable<Supplier> supplierAsQueryable = _supplierRepository.Suppliers;

                int recordsTotal = supplierAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    supplierAsQueryable = supplierAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(contact))
                {
                    supplierAsQueryable = supplierAsQueryable.Where(m => m.Contact.Contains(contact));
                }
                if (!string.IsNullOrWhiteSpace(address))
                {
                    supplierAsQueryable = supplierAsQueryable.Where(m => m.Address.Contains(address));
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    supplierAsQueryable = supplierAsQueryable.Where(m => m.Description.Contains(description));
                }


                int recordsFiltered = supplierAsQueryable.Count();

                var suppliers = await supplierAsQueryable.Select(m => new
                {
                    m.SupplierID,
                    m.Name,
                    m.Contact,
                    m.Address,
                    m.Description,
              
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = suppliers
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
