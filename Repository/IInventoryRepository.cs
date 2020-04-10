using System;

namespace Repository
{
    public interface IInventoryRepository : IRepository<DataLogic.Models.InventoryDL, Guid>
    {
    }
}