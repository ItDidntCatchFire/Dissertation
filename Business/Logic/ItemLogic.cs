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
            var type = await _itemRepository.GetByIdAsync(itemId);

            return new Models.Item()
            {
                ItemId = type.ItemId,
                Name = type.Name,
                Description = type.Description,
                ShelfLife = type.ShelfLife,
                BuyPrice = type.BuyPrice,
                SellPrice = type.SellPrice
            };
        }

        public async Task<Guid> InsertAsync(Models.Item type)
        {
            var itemDL = new DataLogic.Models.ItemDL()
            {
                ItemId = type.ItemId,
                Name = type.Name,
                Description = type.Description,
                ShelfLife = type.ShelfLife,
                BuyPrice = type.BuyPrice,
                SellPrice = type.SellPrice 
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