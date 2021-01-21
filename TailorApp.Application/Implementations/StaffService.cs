using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task CreateAsync(Staff Staff) => await _staffRepository.CreateAsync(Staff);
       
        public async Task DeleteAsync(Staff Staff)=> await _staffRepository.DeleteAsync(Staff);

        public async Task<Staff> FindByIdAsync(int? id)=> await _staffRepository.FindByIdAsync(id);
       
        public async Task<List<Staff>> GetListAsync()=> await _staffRepository.GetListAsync();
       
        public bool IsExists(int id)=> _staffRepository.IsExists(id);
       
        public async Task UpdateAsync(Staff Staff)=> await _staffRepository.UpdateAsync(Staff);
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
                string nID = dataTableDto.Columns[4].Search.Value;
                string registerDate = dataTableDto.Columns[5].Search.Value;
                DateTime time;

                IQueryable<Staff> staffAsQueryable = _staffRepository.Staffs;

                int recordsTotal = staffAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    staffAsQueryable = staffAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(phone))
                {
                    staffAsQueryable = staffAsQueryable.Where(m => m.Phone.Contains(phone));
                }
                if (!string.IsNullOrWhiteSpace(address))
                {
                    staffAsQueryable = staffAsQueryable.Where(m => m.Name.Contains(address));
                }
                if (!string.IsNullOrWhiteSpace(nID))
                {
                    staffAsQueryable = staffAsQueryable.Where(m => m.NID.Contains(nID));
                }

                if (!string.IsNullOrWhiteSpace(registerDate) && DateTime.TryParse(registerDate,out time))
                {
                    staffAsQueryable = staffAsQueryable.Where(m => m.RegisterDate==time);
                }


                int recordsFiltered = staffAsQueryable.Count();

                var staffs = await staffAsQueryable.Select(m => new
                {
                    m.StaffID,
                    m.Name,
                    m.Phone,
                    m.Address,
                    m.NID,
                    RegisterDate = m.RegisterDate.ToShortDateString(),
                   
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = staffs
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }

    }
}
