using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class InventoryScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI () {
		if(kumakore.user ().hasSessionId()) {
			// Buttons / actions
			if(GUI.Button (new Rect(10,10,200,60),"Load inventory")) kumakore.user ().inventory().get().async (delegate(ActionInventoryGet action) {
				Debug.Log ("Inventory loaded");
			});
			if(GUI.Button (new Rect(10,80,200,60),"Load App products")) kumakore.productList ().get ().async (delegate(ActionAppProductListGet action) {
				Debug.Log ("Products loaded");
			});
			if(kumakore.productList().Count > 0) {
				if(GUI.Button (new Rect(10,150,200,60),"Buy random")) kumakore.productList().buyItem(kumakore.productList()[Random.Range (0,kumakore.productList ().Count)].getProductId (),1).async (delegate(ActionAppBuyItem action) {
					Debug.Log ("Item purchased");
				});
				if(GUI.Button (new Rect(10,230,200,60),"Add random")) kumakore.user().inventory().addItem(kumakore.productList()[Random.Range (0,kumakore.productList ().Count)].getProductId(),1).async (delegate(ActionInventoryAdd action) {
					Debug.Log ("Item added");
				});
			}
			// display
			GUI.Box (new Rect(395,5,305,365),"User inventory");
			for(int ii=0; ii< kumakore.user().inventory().Count; ii++) {
				int min = ii * 120;
				ItemBundle item = kumakore.user().inventory ()[ii];
				GUI.Label (new Rect(400,30+min,300,40),"Name: " + item.getName ());
				GUI.Label (new Rect(400,75+min,300,40),"Quantity: " + item.getQuantity ());
			}
			GUI.Box (new Rect(5,310,305,365),"App products");
			for(int ii=0; ii< kumakore.productList().Count; ii++) {
				int min = 310+ii * 120;
				Product prod = kumakore.productList()[ii];
				GUI.Label (new Rect(10,30+min,300,40),"Name: " + prod.getName ());
				int jj = 1;
				IDictionary<string, int> costs = prod.getCostInItems ();
				if(costs != null)
				{
					foreach(KeyValuePair<string,int> pair in costs) {
						GUI.Label (new Rect(10,75*jj+min,300,40),"Cost: '" +pair.Value + " '" + pair.Key + "'");
						jj++;
					}
				}
			}
		} else {
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) kumakore.signin (useremail,password).async (SigninDelegate);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	
	// DELEGATES
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
	}
	
}