using System;

namespace DataLogic.Models
{
    public class InventoryDL
    {
        public Guid InventoryId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
        public bool Export { get; set; }
        public decimal Value { get; set; }
        public bool Ingredient { get; set; }
    }
}