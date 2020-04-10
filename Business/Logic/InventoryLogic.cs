using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class InventoryLogic : ILogic<Models.Inventory, Guid>
    {
        private readonly Repository.IInventoryRepository _inventoryRepository;

        public InventoryLogic(Repository.IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public Task<IEnumerable<Models.Inventory>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Inventory> GetByIdAsync(Guid inventoryId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            return new Models.Inventory()
            {
                InventoryId = inventory.InventoryId,
                ItemId = inventory.ItemId,
                Created = inventory.Created,
                Export = inventory.Export,
                Ingredient = inventory.Ingredient,
                Quantity = inventory.Quantity,
                Value = inventory.Value
            };
        }

        public async Task<Guid> InsertAsync(Models.Inventory inventory)
        {
            var inventoryDL = new DataLogic.Models.InventoryDL()
            {
                ItemId = inventory.ItemId,
                Created = inventory.Created,
                Export = inventory.Export,
                Ingredient = inventory.Ingredient,
                Quantity = inventory.Quantity,
                Value = inventory.Value
            };

            var retVal = await _inventoryRepository.InsertAsync(inventoryDL);
            return retVal;
        }

        public Task DeleteAsync(Models.Inventory type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.Inventory type)
        {
            throw new NotImplementedException();
        }
    }
}