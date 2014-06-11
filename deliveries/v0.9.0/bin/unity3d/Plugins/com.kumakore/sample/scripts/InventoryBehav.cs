using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

namespace com.kumakore.sample {

	public partial class InventoryBehav : SigninBehav {
		
		private static readonly string TAG = typeof(InventoryBehav).Name;	

		public void getInventory() {
			app.getUser ().getInventory().get().async (delegate(ActionInventoryGet action) {
				Kumakore.LOGI (TAG, "Inventory loaded");
			});
		}

		public void getProducts() {
			app.getProducts().get ().async (delegate(ActionInventoryGetProducts action) {
				Kumakore.LOGI (TAG, "Products loaded");
			});
		}

		public void addItem(string id, int quantity) {
			app.getProducts().buyItem(id, quantity).async (delegate(ActionInventoryPurchase action) {
				Kumakore.LOGI (TAG, "Item purchased");
			});
		}
	}
}