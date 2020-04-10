using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;

namespace DataLogic.List
{
    internal static partial class ListStore
    {
        public static List<Models.ItemDL> items = new List<Models.ItemDL>();
    }

    public class ItemDL : Models.ItemDL, IItemRepository
    {
        public Task<IEnumerable<Models.ItemDL>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Models.ItemDL> GetByIdAsync(Guid id)
        {
            return ListStore.items.FirstOrDefault(x => x.ItemId == id);
        }

        public async Task<Guid> InsertAsync(Models.ItemDL type)
        {
            type.ItemId = Guid.NewGuid();
            ListStore.items.Add(type);
            return type.ItemId;
        }

        public Task DeleteAsync(Models.ItemDL type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.ItemDL type)
        {
            throw new NotImplementedException();
        }
    }
}