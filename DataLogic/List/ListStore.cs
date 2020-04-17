using System;
using System.Security.Cryptography;

namespace DataLogic.List
{
    internal static partial class ListStore
    {
        //Put the constructor here for test data
        static ListStore()
        {
            items.Add(new DataLogic.Models.ItemDL()
            {
                ItemId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Stella",
                Description = "Fruity",
                BuyPrice = 1.00m,
                SellPrice = 2.00m,
                ShelfLife = 365
            });

            items.Add(new DataLogic.Models.ItemDL()
            {
                ItemId = Guid.Parse("eaa0ec62-7e0d-454c-966a-171cbb17b0a1"),
                Name = "Guinness",
                Description = "dark",
                BuyPrice = 1.50m,
                SellPrice = 5.00m,
                ShelfLife = 365
            });

            inventory.Add(new DataLogic.Models.InventoryDL()
                {
                    InventoryId = Guid.Parse("5b078b5a-d987-4424-88ea-57f2cca2866e"),
                    ItemId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Created = DateTime.Now,
                    Export = true,
                    Quantity = 5,
                    Value = 1
                }
            );

            inventory.Add(new DataLogic.Models.InventoryDL()
                {
                    InventoryId = Guid.Parse("5b078b5a-d987-4424-88ea-57f2cca2866e"),
                    ItemId = Guid.Parse("eaa0ec62-7e0d-454c-966a-171cbb17b0a1"),
                    Created = DateTime.Now,
                    Export = true,
                    Quantity = 1,
                    Value = 2
                }
            );
            
            inventory.Add(new DataLogic.Models.InventoryDL()
                {
                    InventoryId = Guid.Parse("4da698cc-11a3-4e17-96b1-d3b99c027225"),
                    ItemId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Created = DateTime.Now,
                    Export = true,
                    Quantity = 5,
                    Value = 1
                }
            );
        }
    }
}