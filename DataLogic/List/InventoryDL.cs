using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;

namespace DataLogic.List
{
    internal static partial class ListStore
    {
        public static List<Models.InventoryDL> inventory = new List<Models.InventoryDL>();
    }

    public class InventoryDL : Models.InventoryDL, IInventoryRepository
    {
        public async Task<IEnumerable<Models.InventoryDL>> ListAsync()
            => ListStore.inventory;

        public async Task<IEnumerable<Models.InventoryDL>> GetByIdAsync(Guid id)
            => ListStore.inventory.Where(x => x.InventoryId == id);

        public async Task<IEnumerable<Models.InventoryDL>> InsertAsync(IEnumerable<Models.InventoryDL> type)
        {
            ListStore.inventory.AddRange(type);
            return type;
        }

        public async Task DeleteAsync(IEnumerable<Models.InventoryDL> type)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(IEnumerable<Models.InventoryDL> type)
        {
            throw new NotImplementedException();
        }
    }
}