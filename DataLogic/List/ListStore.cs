using System;

namespace DataLogic.List
{
    internal static partial class ListStore
    {
        //Put the constructor here for test data
        static ListStore()
        {
            items.Add(new DataBase.ItemDL()
            {
                ItemId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Stella",
                Description = "Fruity",
                BuyPrice = 1.00,
                SellPrice = 2.00,
                ShelfLife = 365
            });
        }
    }
}