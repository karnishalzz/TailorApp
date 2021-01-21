using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TailorApp.Application.Dtos.DataTableDtos;
using TailorApp.Application.Services;
using TailorApp.Domain.Entities.RentModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class RentReturnService : IRentReturnService
    {
        private readonly IRentReturnRepository _rentReturnRepository;
        public RentReturnService(IRentReturnRepository rentReturnRepository)
        {
            _rentReturnRepository=rentReturnRepository;
        }

        public async Task CreateAsync(RentReturn rentReturn) => await _rentReturnRepository.CreateAsync(rentReturn);
        public async Task<RentReturn> FindByIdAsync(int id) =>await _rentReturnRepository.FindByIdAsync(id);
        public async Task<List<RentReturn>> GetListAsync() => await _rentReturnRepository.GetListAsync();

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
                string subTotal = dataTableDto.Columns[1].Search.Value;
                string discount = dataTableDto.Columns[2].Search.Value;
                string netTotal = dataTableDto.Columns[3].Search.Value;
                string description = dataTableDto.Columns[4].Search.Value;
                string date = dataTableDto.Columns[5].Search.Value;
                decimal _sub, _discount, _net;
                DateTime time;


                IQueryable<RentReturn> rentAsQueryable = _rentReturnRepository.RentReturns;

                int recordsTotal = rentAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out time))
                {
                    rentAsQueryable = rentAsQueryable.Where(m => m.ReturnedDate == time);
                }

                if (!string.IsNullOrWhiteSpace(subTotal) && decimal.TryParse(subTotal, out _sub))
                {
                    rentAsQueryable = rentAsQueryable.Where(m => m.Subtotal == _sub);
                }
                if (!string.IsNullOrWhiteSpace(discount) && decimal.TryParse(discount, out _discount))
                {
                    rentAsQueryable = rentAsQueryable.Where(m => m.Discount == _discount);
                }
                if (!string.IsNullOrWhiteSpace(netTotal) && decimal.TryParse(netTotal, out _net))
                {
                    rentAsQueryable = rentAsQueryable.Where(m => m.NetTotal == _net);
                }
               
                if (!string.IsNullOrWhiteSpace(description))
                {
                    rentAsQueryable = rentAsQueryable.Where(m => m.Description.Contains(description));
                }


                int recordsFiltered = rentAsQueryable.Count();


                var returns = await rentAsQueryable.Select(m => new
                {
                    m.RentID,
                    m.RentReturnID,
                    m.Subtotal,
                    m.Discount,
                    m.NetTotal,
                    m.Description,
                    ReturnedDate=m.ReturnedDate.ToShortDateString(),
                    

                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = returns
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }


    }
}
