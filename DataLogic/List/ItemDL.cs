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
        public async Task<IEnumerable<Models.ItemDL>> ListAsync()
        {
            return ListStore.items;
        }

        public async Task<Models.ItemDL> GetByIdAsync(Guid id)
        {
            return ListStore.items.FirstOrDefault(x => x.ItemId == id);
        }

        public async Task<Models.ItemDL> InsertAsync(Models.ItemDL type)
        {
            ListStore.items.Add(type);
            return type;
        }

        public Task DeleteAsync(Models.ItemDL type)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Models.ItemDL type)
        {
            var index =  ListStore.items.FindIndex(x => x.ItemId == type.ItemId);
            ListStore.items[index] = type;
        }
    }
}