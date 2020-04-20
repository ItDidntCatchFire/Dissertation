using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IInventoryRepository : IRepository<IEnumerable<DataLogic.Models.InventoryDL>, Guid>
    {
        Task<IEnumerable<DataLogic.Models.InventoryDL>> ListAsync();
    }
}