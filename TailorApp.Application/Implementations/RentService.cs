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
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;

        public RentService(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }
        public int Total => _rentRepository.Total;
        public int Monthly => _rentRepository.Monthly;
        public async Task CreateAsync(Rent rent) => await _rentRepository.CreateAsync(rent);
        public async Task UpdateAsync(Rent rent) => await _rentRepository.UpdateAsync(rent);
        public async Task<Rent> FindByIdAsync(int? id) => await _rentRepository.FindByIdAsync(id);
        public async Task<RentDetail> FindDetailByIdAsync(int id) => await _rentRepository.FindDetailByIdAsync(id);
        public async Task<List<Rent>> GetListAsync() => await _rentRepository.GetListAsync();
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
               
                string customer = dataTableDto.Columns[7].Search.Value;
              


                IQueryable<Rent> rentAsQueryable = _rentRepository.Rents;

                int recordsTotal = rentAsQueryable.Count();


                if (!string.IsNullOrWhiteSpace(customer))
                {
                    rentAsQueryable = rentAsQueryable.Where(m => m.Customer.Name.Contains(customer));
                }


                int recordsFiltered = rentAsQueryable.Count();


                var rents = await rentAsQueryable.Select(m => new
                {
                    m.RentID,
                    RentDate=m.RentDate.ToShortDateString(),
                    ReturnDate=m.ReturnDate.ToShortDateString(),
                    m.Amount,
                    m.Discount,
                    m.GrandTotal,
                    Customer=m.Customer.Name,
                    m.AdvancePayment,
                    m.Paid,
                    m.IsPaid,
                    Returned =m.RentReturns.Sum(x => x.RentReturnDetails.Sum(y => y.Quantity)),//this query throws error
                    Total=m.RentDetails.Sum(x => x.Quantity),
                    IsEqual=(m.RentReturns.Sum(x => x.RentReturnDetails.Sum(y => y.Quantity)) != m.RentDetails.Sum(x => x.Quantity))?true:false,
                    IsDateOver= m.ReturnDate.Date <= DateTime.Now


                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = rents
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }


    }
}
