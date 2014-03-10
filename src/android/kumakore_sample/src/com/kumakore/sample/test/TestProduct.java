package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionInventoryPurchase;
import com.kumakore.ActionInventoryGetProducts;
import com.kumakore.ItemBundle;
import com.kumakore.KumakoreApp;
import com.kumakore.Product;

public class TestProduct extends TestBase {
	private static final String TAG = TestProduct.class.getName();
	
	public TestProduct(KumakoreApp app) {
		super(app);
	}
	
	@Override
	protected void onRun() {
		testProductsGet();	
		testBuyItemWithCoins();
	}
	
	public void testProductsGet() {
		app().getProduct().get().async(new ActionInventoryGetProducts.IKumakore() {

			@Override
			public void onActionInventoryGetProducts(ActionInventoryGetProducts action) {
				for (Product product : app().getProduct()) {
					Log.i(TAG, product.getName() + "  " + product.getProductId());
					testBuyItemWithCoins2();
				}
			}
		});
	}

	public void testBuyItemWithCoins() {
		app().getProduct().buyItem("amazing_powerup", 3).async(new ActionInventoryPurchase.IKumakore() {

			@Override
			public void onActionInventoryPurchase	(ActionInventoryPurchase action) {
				for (ItemBundle item : app().getUser().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}

			}
		});
	}

	public void testBuyItemWithCoins2() {
		Product auxProduct = null;
		for (Product product : app().getProduct()) {
			if (product.getProductId().equals("bomb_pack")) {
				auxProduct = product;
				break;	
			}
		}
		
		if(auxProduct == null)
		{
			Log.w(TAG, "qqweqwe");
			return;
		}
		app().getProduct().buyItem(auxProduct, 1).async(new ActionInventoryPurchase.IKumakore() {

			@Override
			public void onActionInventoryPurchase(ActionInventoryPurchase action) {
				for (ItemBundle item : app().getUser().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}
			}
		});
	}

}
