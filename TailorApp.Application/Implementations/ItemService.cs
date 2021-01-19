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
using TailorApp.Domain.Entities.InventoryModel;
using TailorApp.Domain.Repositories;

namespace TailorApp.Application.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task CreateAsync(Item Item) => await _itemRepository.CreateAsync(Item);

        public async Task DeleteAsync(Item Item) => await _itemRepository.DeleteAsync(Item);

        public async Task<Item> FindByIdAsync(int? id) => await _itemRepository.FindByIdAsync(id);

        public async Task<List<Item>> GetListAsync() => await _itemRepository.GetListAsync();

        public async Task<SelectList> GetSelectListAsync(int? selectedItemId = null) =>
            await _itemRepository.GetSelectListAsync(selectedItemId);

        public bool IsExists(int id) => _itemRepository.IsExists(id);

        public async Task UpdateAsync(Item Item) => await _itemRepository.UpdateAsync(Item);
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
                string unit = dataTableDto.Columns[2].Search.Value;
                string description = dataTableDto.Columns[3].Search.Value;
                string lastUpdated = dataTableDto.Columns[4].Search.Value;

                IQueryable<Item> itemAsQueryable = _itemRepository.Items;

                int recordsTotal = itemAsQueryable.Count();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    itemAsQueryable = itemAsQueryable.Where(m => m.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    itemAsQueryable = itemAsQueryable.Where(m =>m.Unit.ToString().Contains(unit));
                }
                if (!string.IsNullOrWhiteSpace(description))
                {
                    itemAsQueryable = itemAsQueryable.Where(m => m.Description.Contains(description));
                }

                if (!string.IsNullOrWhiteSpace(lastUpdated))
                {
                    itemAsQueryable = itemAsQueryable.Where(m => m.LastUpdated.ToShortDateString().Contains(lastUpdated));
                }


                int recordsFiltered = itemAsQueryable.Count();

                var items = await itemAsQueryable.Select(m => new
                {
                    m.ItemID,
                    m.Name,
                    Unit=m.Unit.Value,
                    m.Description,
                    LastUpdated = m.LastUpdated.ToShortDateString()
                }).OrderBy(sortColumnName + " " + sortColumnDir).Skip(start).Take(length).ToListAsync();

                return new
                {
                    draw,
                    recordsTotal,
                    recordsFiltered,
                    data = items
                };
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
