using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class ItemLogic : ILogic<Models.Item, Guid>
    {
        private readonly Repository.IItemRepository _itemRepository;

        public ItemLogic(Repository.IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Task<IEnumerable<Models.Item>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Item> GetByIdAsync(Guid itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);

            return new Models.Item()
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                ShelfLife = item.ShelfLife
            };
        }

        public async Task<Guid> InsertAsync(Models.Item type)
        {
            var itemDL = new DataLogic.Models.ItemDL()
            {
                ItemId = type.ItemId,
                Name = type.Name,
                Description = type.Description,
                ShelfLife = type.ShelfLife
            };

            var retVal = await _itemRepository.InsertAsync(itemDL);
            return retVal;
        }

        public Task DeleteAsync(Models.Item type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.Item type)
        {
            throw new NotImplementedException();
        }
    }
}