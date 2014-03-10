using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class InventoryScene : MonoBehaviour {
	#region demo variables
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	#endregion
	
	#region Kumakore calls
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
		kumakore.load();
	}
	
	void OnDestroy() {
		kumakore.save ();
	}
	
	private void Update() {
		kumakore.getDispatcher().dispatch();	
	}
	
	// Kumakore calls
	public void SignIn(string user, string pass) {
		kumakore.signin (useremail,password).async (delegate(ActionUserSignin action) {
			message = "Signin delegate: " + action.getStatusMessage();
		});
	}
	public void GetInventory() {
		kumakore.getUser ().getInventory().get().async (delegate(ActionInventoryGet action) {
			Debug.Log ("Inventory loaded");
		});
	}
	public void GetProducts() {
		kumakore.getProducts().get ().async (delegate(ActionInventoryGetProducts action) {
			Debug.Log ("Products loaded");
		});
	}
	public void AddItem(string id, int quantity) {
		kumakore.getProducts().buyItem(id,quantity).async (delegate(ActionInventoryPurchase action) {
			Debug.Log ("Item purchased");
		});
	}
	#endregion
	
	#region GUI
	
	void OnGUI () {
		if(kumakore.getUser ().hasSessionId()) {
			// Buttons / actions
			if(GUI.Button (new Rect(10,10,200,60),"Load inventory")) GetInventory ();
			if(GUI.Button (new Rect(10,80,200,60),"Load App products")) GetProducts();
//			if(	kumakore.products().Count > 0 && 
//				GUI.Button (new Rect(10,230,200,60),"Add random")) 
//					AddItem(kumakore.products()[Random.Range (0,kumakore.products ().Count)].getProductId(),1);
			
			// display
			GUI.Box (new Rect(395,5,305,365),"User inventory");
			
//			for(int ii=0; ii< kumakore.getUser().getInventory().Count; ii++) {
//				int min = ii * 120;
//				ItemBundle item = kumakore.getUser().getInventory ()[ii];
//				GUI.Label (new Rect(400,30+min,300,40),"Name: " + item.getName ());
//				GUI.Label (new Rect(400,75+min,300,40),"Quantity: " + item.getQuantity ());
//			}
			GUI.Box (new Rect(5,310,305,365),"App products");
//			for(int ii=0; ii< kumakore.products().Count; ii++) {
//				int min = 310+ii * 120;
//				Product prod = kumakore.products()[ii];
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
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) SignIn (useremail,password);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	#endregion
	
}