using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

namespace com.kumakore.sample {

	public partial class InventoryBehav : SigninBehav {

		protected override void OnGUI () {
			if(app.getUser ().hasSessionId()) {
				// Buttons / actions
				if(GUI.Button (new Rect(10,10,200,60),"Load inventory")) 
					getInventory ();

				if(GUI.Button (new Rect(10,80,200,60),"Load App products")) 
					getProducts();
	//			if(	app.products().Count > 0 && 
	//				GUI.Button (new Rect(10,230,200,60),"Add random")) 
	//					AddItem(app.products()[Random.Range (0,app.products ().Count)].getProductId(),1);
				
				// display
				GUI.Box (new Rect(395,5,305,365),"User inventory");
				
	//			for(int ii=0; ii< app.getUser().getInventory().Count; ii++) {
	//				int min = ii * 120;
	//				ItemBundle item = app.getUser().getInventory ()[ii];
	//				GUI.Label (new Rect(400,30+min,300,40),"Name: " + item.getName ());
	//				GUI.Label (new Rect(400,75+min,300,40),"Quantity: " + item.getQuantity ());
	//			}
				GUI.Box (new Rect(5,310,305,365),"App products");
	//			for(int ii=0; ii< app.products().Count; ii++) {
	//				int min = 310+ii * 120;
	//				Product prod = app.products()[ii];
	//				GUI.Label (new Rect(10,30+min,300,40),"Name: " + prod.getName ());
	//				int jj = 1;
	//				IDictionary<string, int> costs = prod.getCostInItems ();
	//				if(costs != null)
	//				{
	//					foreach(KeyValuePair<string,int> pair in costs) {
	//						GUI.Label (new Rect(10,75*jj+min,300,40),"Cost: '" +pair.Value + " '" + pair.Key + "'");
	//						jj++;
	//					}
	//				}
	//			}
			} else {
				base.OnGUI();
			}

			if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
		}
		
	}
}