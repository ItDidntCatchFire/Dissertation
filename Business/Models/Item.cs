﻿using System;

namespace Business.Models
{
    public class Item : IModel
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ShelfLife { get; set; }

        public bool IsValid()
        {
            if (ItemId.ToString() != "")
                if (Name != "")
                    if (Description != "")
                        if (ShelfLife != 0)
                            return true;
            return false;
        }
    }
}