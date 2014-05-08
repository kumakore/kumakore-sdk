package com.kumakore.sample;

import java.util.ArrayList;
import java.util.List;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.SeekBar;
import android.widget.SeekBar.OnSeekBarChangeListener;
import android.widget.TextView;
import android.widget.Toast;

import com.kumakore.ActionInventoryPurchase;
import com.kumakore.ActionInventoryGetProducts;
import com.kumakore.ActionInventoryAdd;
import com.kumakore.ActionInventoryGet;
import com.kumakore.ActionInventoryRemove;
import com.kumakore.InventoryMap;
import com.kumakore.ItemBundle;
import com.kumakore.Product;
import com.kumakore.ProductMap;
import com.kumakore.StatusCodes;
import com.kumakore.listactivity.ItemArrayAdapter;

public class InventoryActivity extends KumakoreSessionActivity implements ActionInventoryAdd.IKumakore,
		ActionInventoryRemove.IKumakore, ActionInventoryGet.IKumakore, ActionInventoryPurchase.IKumakore,
		ActionInventoryGetProducts.IKumakore {
	private static final String TAG = InventoryActivity.class.getName();

	private TextView amountLB;
	private ListView listViewItems;
	private ItemArrayAdapter itemsAdapter;
	private SeekBar amountBar;

	private int itemAmount;

	private boolean inventoryDownloaded, productsDownloaded;

	private ProductMap _productList;
	private InventoryMap _inventory;

	private Product _selectedProduct;

	private ProgressDialog _dialog;

	public InventoryActivity() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_inventory);

		itemAmount = 1;

		getWidgets();
		drawButtons();

		downloadProducts();
	}

	private void downloadProducts(){
		_dialog = ProgressDialog.show(InventoryActivity.this, "", "Loading. Please wait...", true);
		getInventory();
		getProducts();
	}
	
	private void getWidgets() {
		listViewItems = (ListView) findViewById(R.id.inventory_listItems);

		amountBar = (SeekBar) findViewById(R.id.seekBarInventory);
	}

	private void drawButtons() {

		amountLB = (TextView) findViewById(R.id.textAmount);
		amountLB.setText("Amount: " + itemAmount);
		
		amountBar.setProgress(itemAmount);
		amountBar.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {

			@Override
			public void onStopTrackingTouch(SeekBar seekBar) {
				itemAmount = seekBar.getProgress() + 1;
				amountLB.setText("Amount: " + itemAmount);
			}

			@Override
			public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
				itemAmount = seekBar.getProgress() + 1;
				amountLB.setText("Amount: " + itemAmount);
			}

			@Override
			public void onStartTrackingTouch(SeekBar seekBar) {

			}
		});

		Button btAddItem =(Button) findViewById(R.id.buttonAddItem);		
		btAddItem.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				addItem();
			}
		});

		Button btRemoveItem = (Button) findViewById(R.id.buttonRemoveItem);
		btRemoveItem.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				removeItem();
			}
		});

		Button btBuyWithCoins =(Button) findViewById(R.id.buttonBuyCoins);
		btBuyWithCoins.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				buyItemWithCoins();
			}
		});

		Button btPurchase = (Button) findViewById(R.id.buttonPurchaseIAP);
		btPurchase.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				purchaseItem();
			}
		});

	}

	private void addItem() {
		if (_selectedProduct == null) {
			Toast toast = Toast.makeText(this, "Select a product", Toast.LENGTH_SHORT);
			toast.show();
			return;
		}
		app().getUser().inventory().addItem(_selectedProduct, itemAmount).async(InventoryActivity.this);
	}

	private void removeItem() {
		if (_selectedProduct == null) {
			Toast toast = Toast.makeText(this, "Select a product", Toast.LENGTH_SHORT);
			toast.show();
			return;
		}
		ItemBundle item = _inventory.findItemBundle(_selectedProduct.getProductId());
		if (item == null) {
			Toast toast = Toast.makeText(this, "You don't have this item to remove", Toast.LENGTH_SHORT);
			toast.show();
			return;
		}
		app().getUser().inventory().removeItem(item.getName(), itemAmount).sync(InventoryActivity.this);

	}

	private void buyItemWithCoins() {
		if (_selectedProduct == null) {
			Toast toast = Toast.makeText(this, "Select a product", Toast.LENGTH_SHORT);
			toast.show();
			return;
		}

		app().getProduct().buyItem(_selectedProduct.getProductId(), itemAmount).async(InventoryActivity.this);
	}

	private void purchaseItem() {
		if (_selectedProduct == null) {
			Toast toast = Toast.makeText(this, "Select a product", Toast.LENGTH_SHORT);
			toast.show();
			return;
		}

		// _app.productList().buyItem(_selectedProduct.getProductId(),
		// itemAmount).async(InventoryActivity.this);
	}

	private void fillItemList() {

		List<String> titles = new ArrayList<String>();
		List<String> descriptions = new ArrayList<String>();
		List<Product> productList = new ArrayList<Product>();
		ItemBundle itemBundle;
		String desc = "";
		for (Product product : _productList) {
			itemBundle = _inventory.findItemBundle(product.getProductId());
			if (itemBundle != null)
				titles.add(product.getName() + " " + itemBundle.getQuantity());
			else
				titles.add(product.getName() + " 0");

			if (product.getCostInItems().size() > 0 || !product.getCostInDollars().isEmpty()) {
				desc = "Buy with ";
				if (product.getCostInItems().size() > 0) {
					desc += product.getCostInItems().get(Helpers.VIRTUAL_STORE_ITEM) + " "
							+ Helpers.VIRTUAL_STORE_ITEM;
				}
				if (!product.getCostInDollars().isEmpty()) {
					if (product.getCostInItems().size() > 0)
						desc += " | ";
					desc += "$" + product.getCostInDollars();
				}
			}
			descriptions.add(desc);
			productList.add(product);
		}

		// View view = findViewById(android.R.layout.activity_list_item);
		itemsAdapter = new ItemArrayAdapter(this, titles, descriptions, productList);

		listViewItems.setAdapter(itemsAdapter);
		listViewItems.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
		listViewItems.setFocusableInTouchMode(true);

		// handle click on match
		listViewItems.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			public void onItemClick(AdapterView<?> parentAdapter, View view, int position, long id) {
				Log.i(TAG, "position " + position);

				_selectedProduct = _productList.get(position);
				listViewItems.setSelection(position);
				itemsAdapter.setHighlighted(position);
				itemsAdapter.notifyDataSetChanged();
			}
		});

	}

	private void getProducts() {
		productsDownloaded = false;
		app().getProduct().get().async(InventoryActivity.this);
	}

	public void getInventory() {
		inventoryDownloaded = false;
		app().getUser().inventory().get().async(this);
	}

	private void checkDownload() {

		if (productsDownloaded && inventoryDownloaded) {
			fillItemList();
			_dialog.dismiss();
		}
	}

	private void handleInventoryResponse(StatusCodes status, String message) {
		if (status == StatusCodes.SUCCESS) {
			inventoryDownloaded = true;
			_inventory = app().getUser().inventory();
			checkDownload();
		} else {
			Toast toast = Toast.makeText(this, message, Toast.LENGTH_SHORT);
			toast.show();
		}
	}

	@Override
	public void onActionInventoryAdd(ActionInventoryAdd action) {
		handleInventoryResponse(action.getStatusCode(), action.getStatusMessage());
	}

	@Override
	public void onActionInventoryGet(ActionInventoryGet action) {
		handleInventoryResponse(action.getStatusCode(), action.getStatusMessage());
	}

	@Override
	public void onActionInventoryRemove(ActionInventoryRemove action) {
		handleInventoryResponse(action.getStatusCode(), action.getStatusMessage());
	}

	@Override
	public void onActionInventoryPurchase(ActionInventoryPurchase action) {
		handleInventoryResponse(action.getStatusCode(), action.getStatusMessage());
	}

	@Override
	public void onActionInventoryGetProducts(ActionInventoryGetProducts action) {
		productsDownloaded = true;
		_productList = app().getProduct();
		checkDownload();
	}

}
