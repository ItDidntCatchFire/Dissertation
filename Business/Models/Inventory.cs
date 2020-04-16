using System;

namespace Business.Models
{
    public class Inventory : IModel
    {
        public Guid InventoryId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
        public bool Export { get; set; }
        public decimal Value { get; set; }

        public bool IsValid()
        {
            if (ItemId != Guid.Empty)
                if (Quantity != 0)
                    return true;
            return false;
        }
    }
}