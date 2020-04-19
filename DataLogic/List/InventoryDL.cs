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

        public async Task<Models.InventoryDL> GetByIdAsync(Guid id)
            => ListStore.inventory.FirstOrDefault(x => x.InventoryId == id);

        public async Task<Models.InventoryDL> InsertAsync(Models.InventoryDL type)
        {
            ListStore.inventory.Add(type);
            return type;
        }

        public async Task DeleteAsync(Models.InventoryDL type)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Models.InventoryDL type)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> InsertListAsync(IEnumerable<Models.InventoryDL> types)
        {
            ListStore.inventory.AddRange(types);

            //They all have the same ID, so can just return the first
            return types.First().InventoryId;
        }
    }
}