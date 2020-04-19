using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Item : IModel
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ShelfLife { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }

        public Item()
        {
            
        }
        
        public Item(Item item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
            Description = item.Description;
            ShelfLife = item.ShelfLife;
            BuyPrice = item.BuyPrice;
            SellPrice = item.SellPrice;
        }
        
        public IEnumerable<string> Validate()
        {
            var invalidReasons = new List<string>();

            if (String.IsNullOrEmpty(Name) && String.IsNullOrWhiteSpace(Name))
                invalidReasons.Add("Name missing");
            
            if (String.IsNullOrEmpty(Description) && String.IsNullOrWhiteSpace(Description))
                invalidReasons.Add("Description missing");
            
            if (ShelfLife <= -1)
                invalidReasons.Add("ShelfLife missing");
            
            if (BuyPrice <= -1)
                invalidReasons.Add("BuyPrice missing");
            
            if (SellPrice <= -1)
                invalidReasons.Add("SellPrice missing");
            
            return invalidReasons;
        }
    }
}