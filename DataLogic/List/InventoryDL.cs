using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;

namespace DataLogic.List
{
    internal static partial class ListStore
    {
        public static List<Models.InventoryDL> Inventory = new List<Models.InventoryDL>();
    }

    public class InventoryDL : IInventoryRepository<Models.InventoryDL, Guid>
    {
        public async Task<IEnumerable<Models.InventoryDL>> ListAsync()
            => ListStore.Inventory;

        public async Task<IEnumerable<Models.InventoryDL>> GetByIdAsync(Guid id)
            => ListStore.Inventory.Where(x => x.InventoryId == id);

        public async Task<IEnumerable<Models.InventoryDL>> InsertAsync(IEnumerable<Models.InventoryDL> inventories)
        {
            ListStore.Inventory.AddRange(inventories);
            return inventories;
        }

        public async Task DeleteAsync(IEnumerable<Models.InventoryDL> type)
            => throw new NotImplementedException();

        public async Task UpdateAsync(IEnumerable<Models.InventoryDL> type)
            => throw new NotImplementedException();
    }
}