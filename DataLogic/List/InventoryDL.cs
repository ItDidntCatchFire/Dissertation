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
        {
            throw new NotImplementedException();
        }

        public async Task<Models.InventoryDL> GetByIdAsync(Guid id)
        {
            return ListStore.inventory.FirstOrDefault(x => x.InventoryId == id);
        }

        public async Task<Guid> InsertAsync(Models.InventoryDL type)
        {
            type.InventoryId = Guid.NewGuid();
            ListStore.inventory.Add(type);
            return type.InventoryId;
        }

        public async Task DeleteAsync(Models.InventoryDL type)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Models.InventoryDL type)
        {
            throw new NotImplementedException();
        }
    }
}