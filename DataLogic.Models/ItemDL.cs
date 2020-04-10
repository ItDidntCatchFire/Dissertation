using System;

namespace DataLogic.Models
{
    public class ItemDL
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ShelfLife { get; set; }
    }
}