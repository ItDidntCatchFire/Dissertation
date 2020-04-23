using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class ItemLogic : ILogic<Models.Item, Guid>
    {
        private readonly Repository.IItemRepository<DataLogic.Models.ItemDL,Guid> _itemRepository;

        public ItemLogic(Repository.IItemRepository<DataLogic.Models.ItemDL,Guid> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Models.Item>> ListAsync()
        {
            var itemDLs = await _itemRepository.ListAsync();
            var retVal = new List<Models.Item>();
            foreach (var item in itemDLs)
                retVal.Add(new Models.Item()
                {
                    ItemId =  item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    BuyPrice = item.BuyPrice,
                    SellPrice = item.SellPrice,
                    ShelfLife = item.ShelfLife
                });
            
            return retVal;
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

        public async Task<Models.Item> InsertAsync(Models.Item item)
        {
            var itemDL = new DataLogic.Models.ItemDL()
            {
                ItemId = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                ShelfLife = item.ShelfLife,
                BuyPrice = item.BuyPrice,
                SellPrice = item.SellPrice
            };

            var retVal = await _itemRepository.InsertAsync(itemDL);
            
            return new Models.Item()
            {
                ItemId = retVal.ItemId,
                Name = retVal.Name,
                Description = retVal.Description,
                ShelfLife = retVal.ShelfLife,
                BuyPrice = retVal.BuyPrice,
                SellPrice = retVal.SellPrice
            };
        }

        public Task DeleteAsync(Models.Item type)
            => throw new NotImplementedException();

        public async Task UpdateAsync(Models.Item type)
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

            await _itemRepository.UpdateAsync(itemDL);
        }
        
        public async Task<List<Models.Item>> InsertListAsync(IEnumerable<Models.Item> items)
        {
            var itemDLs = new List<DataLogic.Models.ItemDL>();
            foreach (var item in items)
                itemDLs.Add(new DataLogic.Models.ItemDL()
                {
                    ItemId = item.ItemId == Guid.Empty ? Guid.NewGuid() : item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    ShelfLife = item.ShelfLife,
                    BuyPrice = item.BuyPrice,
                    SellPrice = item.SellPrice
                });
            
            itemDLs = (await _itemRepository.InsertListAsync(itemDLs)).ToList();

            var retVal = new List<Models.Item>();
            foreach (var item in itemDLs)
                retVal.Add(new Models.Item()
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    ShelfLife = item.ShelfLife,
                    BuyPrice = item.BuyPrice,
                    SellPrice = item.SellPrice
                });
            return retVal;
        }
    }
}