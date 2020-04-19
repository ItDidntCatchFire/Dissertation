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

        public async Task<IEnumerable<Models.Inventory>> ListAsync()
        {
            var inventoryDls = await _inventoryRepository.ListAsync();

            var retVal = new List<Models.Inventory>();
            foreach (var inventory in inventoryDls)
                retVal.Add(new Models.Inventory()
                {
                    InventoryId = inventory.InventoryId,
                    ItemId = inventory.ItemId,
                    Time = inventory.Created,
                    Export = inventory.Export,
                    Quantity = inventory.Quantity,
                    Value = inventory.Value
                });

            return retVal;
        }

        public async Task<Models.Inventory> GetByIdAsync(Guid inventoryId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            return new Models.Inventory()
            {
                InventoryId = inventory.InventoryId,
                ItemId = inventory.ItemId,
                Time = inventory.Created,
                Export = inventory.Export,
                Quantity = inventory.Quantity,
                Value = inventory.Value
            };
        }

        public async Task<Models.Inventory> InsertAsync(Models.Inventory inventory)
        {
            var inventoryId = Guid.NewGuid();
            var time = DateTime.Now;
            var inventoryDL = new DataLogic.Models.InventoryDL()
            {
                InventoryId = inventoryId,
                ItemId = inventory.ItemId,
                Created = time,
                Export = inventory.Export,
                Quantity = inventory.Quantity,
                Value = inventory.Value
            };

            var retVal = await _inventoryRepository.InsertAsync(inventoryDL);
            
            return new Models.Inventory()
            {
                InventoryId = retVal.InventoryId,
                ItemId = retVal.ItemId,
                Time = retVal.Created,
                Export = retVal.Export,
                Quantity = retVal.Quantity,
                Value = retVal.Value
            };
        }

        public Task DeleteAsync(Models.Inventory type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.Inventory type)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> InsertListAsync(IEnumerable<Models.Inventory> inventories)
        {
            var invId = Guid.NewGuid();

            var inventoryDLs = new List<DataLogic.Models.InventoryDL>();
            foreach (var inventory in inventories)
                inventoryDLs.Add(new DataLogic.Models.InventoryDL()
                {
                    InventoryId = invId,
                    ItemId = inventory.ItemId,
                    Created = inventory.Time,
                    Export = inventory.Export,
                    Quantity = inventory.Quantity,
                    Value = inventory.Value
                });

            var retVal = await _inventoryRepository.InsertListAsync(inventoryDLs);
            return retVal;
        }
    }
}