using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
