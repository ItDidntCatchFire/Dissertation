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

    public class ItemDL : IItemRepository<Models.ItemDL, Guid>
    {
        public async Task<IEnumerable<Models.ItemDL>> ListAsync()
        {
            return ListStore.items;
        }

        public async Task<Models.ItemDL> GetByIdAsync(Guid id)
        {
            return ListStore.items.FirstOrDefault(x => x.ItemId == id);
        }

        public async Task<Models.ItemDL> InsertAsync(Models.ItemDL item)
        {
            ListStore.items.Add(item);
            return item;
        }

        public Task DeleteAsync(Models.ItemDL type)
            => throw new NotImplementedException();

        public async Task UpdateAsync(Models.ItemDL item)
        {
            var index =  ListStore.items.FindIndex(x => x.ItemId == item.ItemId);
            ListStore.items[index] = item;
        }
        
        public async Task<IEnumerable<Models.ItemDL>> InsertListAsync(IEnumerable<Models.ItemDL> items)
        {
            ListStore.items.AddRange(items);
            return items;
        }
    }
}