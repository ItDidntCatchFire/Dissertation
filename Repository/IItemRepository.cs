using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IItemRepository : IRepository<DataLogic.Models.ItemDL, Guid>
    {
        Task<IEnumerable<DataLogic.Models.ItemDL>> ListAsync();
    }
}