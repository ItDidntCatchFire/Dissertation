using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class InventoryLogic : ILogic<IEnumerable<Models.Inventory>, Guid>
    {
        private readonly Repository.IInventoryRepository _inventoryRepository;

        public InventoryLogic(Repository.IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public async Task<IEnumerable<Models.Inventory>> ListAsync()
        {
            var inventoryDLs = await _inventoryRepository.ListAsync();
            
            var retVal = new List<Models.Inventory>();
            
            foreach (var inventoryDL in inventoryDLs)
               retVal.Add(new Models.Inventory()
                    {
                        InventoryId = inventoryDL.InventoryId,
                        ItemId = inventoryDL.ItemId,
                        Time = inventoryDL.Time,
                        Export = inventoryDL.Export,
                        Quantity = inventoryDL.Quantity,
                        Monies = inventoryDL.Monies
                    });

            return retVal;
        }

        public async Task<IEnumerable<Models.Inventory>> GetByIdAsync(Guid inventoryId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
            
            var inventories = new List<Models.Inventory>();
            foreach (var inv in inventory)
            {
                inventories.Add(new Models.Inventory()
                {
                    InventoryId = inv.InventoryId,
                    ItemId = inv.ItemId,
                    Time = inv.Time,
                    Export = inv.Export,
                    Quantity = inv.Quantity,
                    Monies = inv.Monies
                });
            }

            return inventories;
        }

        public async Task<IEnumerable<Models.Inventory>> InsertAsync(IEnumerable<Models.Inventory> inventory)
        {
            var invId = Guid.NewGuid();

            var inventoryDLs = new List<DataLogic.Models.InventoryDL>();
            foreach (var inven in inventory)
                inventoryDLs.Add(new DataLogic.Models.InventoryDL()
                {
                    InventoryId = inven.InventoryId == Guid.Empty ? invId : inven.InventoryId,
                    ItemId = inven.ItemId,
                    Time = inven.Time,
                    Export = inven.Export,
                    Quantity = inven.Quantity,
                    Monies = inven.Monies
                });

            var inventoryDls = await _inventoryRepository.InsertAsync(inventoryDLs);
            
            var retVal = new List<Models.Inventory>();
            foreach (var inv in inventoryDls)
                retVal.Add(new Models.Inventory()
                {
                    InventoryId = inv.InventoryId,
                    ItemId = inv.ItemId,
                    Time = inv.Time,
                    Export = inv.Export,
                    Quantity = inv.Quantity,
                    Monies = inv.Monies
                });
            
            return retVal;
        }

        public Task DeleteAsync(IEnumerable<Models.Inventory> type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<Models.Inventory> type)
        {
            throw new NotImplementedException();
        }
    }
}