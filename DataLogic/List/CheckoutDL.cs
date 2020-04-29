using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLogic.List {

	internal static partial class ListStore {
		public static List<Models.InventoryDL> Checkout = new List<Models.InventoryDL>();
	}

	public class CheckoutDL : Repository.ICheckoutRepository<Models.InventoryDL, Guid> {
		public async Task<IEnumerable<Models.InventoryDL>> ListAsync()
			=> ListStore.Checkout;

		public async Task ClearAsync() {
			ListStore.Checkout.Clear();
		}

		public async Task InsertListAsync(IEnumerable<Models.InventoryDL> inventories) {
			ListStore.Checkout.AddRange(inventories);
		}
	}
}
