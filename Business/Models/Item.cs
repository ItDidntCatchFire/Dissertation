namespace Business.Models {
	public class Item {
		public int ItemId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }


		public bool IsValid() {
			if (ItemId > 0)
				if (Name != "")
					if (Description != "")
						return true;

			return false;

		}
	}
}
