using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateAsync(Customer customer)=> await _customerRepository.CreateAsync(customer);
        
        public async Task DeleteAsync(Customer customer)=> await _customerRepository.DeleteAsync(customer);

        public async Task<Customer> FindByIdAsync(int? id)=> await _customerRepository.FindByIdAsync(id);

        public async Task<List<Customer>> GetListAsync()=> await _customerRepository.GetListAsync();
        public bool IsExists(int id)=> _customerRepository.IsExists(id);
      
        public async Task UpdateAsync(Customer Customer)=> await _customerRepository.UpdateAsync(Customer);
        public async Task<SelectList> GetSelectListAsync(int? selectedCustomerId = null)=>
            await _customerRepository.GetSelectListAsync(selectedCustomerId);

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
                string phone = dataTableDto.Columns[2].Search.Value;
                string address = dataTableDto.Columns[3].Search.Value;
                string registerDate = dataTableDto.Columns[4].Search.Value;

                IQueryable<Customer> customerAsQueryable = _customerRepository.Customers;

                int recordsTotal = customerAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    customerAsQueryable = customerAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(phone))
                {
                    customerAsQueryable = customerAsQueryable.Where(m => m.Phone.Contains(phone));
                }
                if (!string.IsNullOrWhiteSpace(address))
                {
                    customerAsQueryable = customerAsQueryable.Where(m => m.Name.Contains(address));
                }

                if (!string.IsNullOrWhiteSpace(registerDate))
                {
                    customerAsQueryable = customerAsQueryable.Where(m => m.RegisterDate.ToShortDateString().Contains(registerDate));
                }


                int recordsFiltered = customerAsQueryable.Count();

                var customers = await customerAsQueryable.Select(m => new
                {
                    m.CustomerID,
                    m.Name,
                    m.Phone,
                    m.Address,
                    RegisterDate = m.RegisterDate.ToShortDateString()
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = customers
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
