using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Inventory : IModel
    {
        public Guid InventoryId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
        public bool Export { get; set; }
        public decimal Value { get; set; }

        public IEnumerable<string> Validate()
        {
            var invalidReasons = new List<string>();
            
            if (Quantity <= -1)
                invalidReasons.Add("Quantity missing");
            
            return invalidReasons;
        }
    }
}