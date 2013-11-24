package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionInventoryAdd;
import com.kumakore.ActionInventoryGet;
import com.kumakore.ActionInventoryRemove;
import com.kumakore.ItemBundle;
import com.kumakore.KumakoreApp;

public class TestInventory extends TestBase {
	private static final String TAG = TestInventory.class.getName();

	public TestInventory(KumakoreApp app) {
		super(app);
	}
	
	@Override
	protected void onRun() {
		testInventoryGet();
		testInventoryAdd();
		testInventoryRemove();			
	}
	
	public void testInventoryGet() {
		app().user().inventory().get().async(new ActionInventoryGet.IKumakore() {

			@Override
			public void onActionInventoryGet(ActionInventoryGet action) {
				// Log.i(TAG, "Inventory size " +
				// app.user().inventory().size());
				for (ItemBundle item : app().user().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}
			}
		});
	}

	public void testInventoryAdd() {

		app().user().inventory().addItem("amazing_powerup", 1).async(new ActionInventoryAdd.IKumakore() {

			@Override
			public void onActionInventoryAdd(ActionInventoryAdd action) {
				Log.i(TAG, "Inventory size " + app().user().inventory().size());
				for (ItemBundle item : app().user().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}

			}
		});
	}

	public void testInventoryRemove() {

		app().user().inventory().removeItem("amazing_powerup", 1).async(new ActionInventoryRemove.IKumakore() {

			@Override
			public void onActionInventoryRemove(ActionInventoryRemove action) {
				Log.i(TAG, "Inventory size " + app().user().inventory().size());
				for (ItemBundle item : app().user().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}

			}
		});

	}
}
