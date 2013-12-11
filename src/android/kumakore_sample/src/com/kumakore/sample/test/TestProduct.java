package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionAppBuyItem;
import com.kumakore.ActionAppProductListGet;
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
		app().productList().get().async(new ActionAppProductListGet.IKumakore() {

			@Override
			public void onActionAppProductListGet(ActionAppProductListGet action) {
				for (Product product : app().productList()) {
					Log.i(TAG, product.getName() + "  " + product.getProductId());
					testBuyItemWithCoins2();
				}
			}
		});
	}

	public void testBuyItemWithCoins() {
		app().productList().buyItem("amazing_powerup", 3).async(new ActionAppBuyItem.IKumakore() {

			@Override
			public void onActionAppBuyItem(ActionAppBuyItem action) {
				for (ItemBundle item : app().user().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}

			}
		});
	}

	public void testBuyItemWithCoins2() {
		Product auxProduct = null;
		for (Product product : app().productList()) {
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
		app().productList().buyItem(auxProduct, 1).async(new ActionAppBuyItem.IKumakore() {

			@Override
			public void onActionAppBuyItem(ActionAppBuyItem action) {
				for (ItemBundle item : app().user().inventory()) {
					Log.i(TAG, item.getProductId() + "  " + item.getQuantity());
				}
			}
		});
	}

}
