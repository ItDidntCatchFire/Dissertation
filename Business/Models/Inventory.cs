using System;
using System.Collections.Generic;

namespace Business.Models {
	public class Inventory : IModel {
		public Guid InventoryId { get; set; }
		public Guid ItemId { get; set; }
		public int Quantity { get; set; }
		public DateTime Time { get; set; }
		public bool Export { get; set; }
		public decimal Monies { get; set; }

		public IEnumerable<string> Validate() {
			var invalidReasons = new List<string>();

			if (ItemId == Guid.Empty)
				invalidReasons.Add("ItemId Empty");

			if (Quantity < 0)
				invalidReasons.Add("Quantity negative");
			if (Quantity == 0)
				invalidReasons.Add("Quantity zero");

			if (Monies <= -1)
				invalidReasons.Add("Monies negative");

			return invalidReasons;
		}
	}
}