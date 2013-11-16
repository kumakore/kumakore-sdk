using UnityEngine;
using System.Collections;
using com.kumakore;

public class UserScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	private bool isRegistered = false;
	
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI () {
		if(kumakore.user ().hasId()) {
			// User info actions
			GUI.Label(new Rect(10,10,400,60),"Signed in");
			if(GUI.Button (new Rect(10,90,200,60),"Load user")) kumakore.user ().get ().sync(GetUserDelegate);
			// device
			if(isRegistered) {
				if(GUI.Button (new Rect(10,170,200,60),"Unregister device")) kumakore.user ().device().unregister().sync(UnregisterDeviceDelegate);
				if(GUI.Button (new Rect(10,250,200,60),"Mute")) kumakore.user ().device().mute().sync(MuteDeviceDelegate);
				if(GUI.Button (new Rect(240,250,200,60),"Unmute")) kumakore.user ().device().unmute().sync(UnmuteDeviceDelegate);
				
			} else if(GUI.Button (new Rect(10,170,200,60),"Register device")) kumakore.user ().device().register("senderId").sync(RegisterDeviceDelegate);
				
			if(GUI.Button (new Rect(10,330,200,60),"Get Info")) message = "email:"+ kumakore.user ().getEmail() + "|" +
																			"facebookID:"+kumakore.user ().getFacebookId() + "|" +
																			"id:"+kumakore.user ().getId() + "|" +
																			"name:"+kumakore.user ().getName()+ "|" +
																			//"sessionID:"+kumakore.user ().getSessionId()+ "|" + //SESSION is internal
																			"push enabled:"+kumakore.user ().getPushEnabled();
				
		} else {
			// SIgn in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) kumakore.signin (useremail,password).async (SigninDelegate);
		}
	
		GUI.Label (new Rect(10,600,600,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	
	// DELEGATES
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
	}
	public void GetUserDelegate(ActionUserGet action) {
		if(action.getCode() == StatusCodes.SUCCESS)
			message = "Get user delegate: " + kumakore.user ().getName () + ", " + kumakore.user ().getEmail();
		else message = "Get user delegate: " + action.getStatusMessage();
	}
	public void RegisterDeviceDelegate(ActionDeviceRegister action,string token) {
		message = "Register delegate: " + action.getStatusMessage();
		isRegistered = true;
	}
	public void UnregisterDeviceDelegate(ActionDeviceUnregister action) {
		message = "Unregister delegate: " + action.getStatusMessage();
		isRegistered = false;
	}
	public void MuteDeviceDelegate(ActionDeviceMute action) {
		message = "Mute delegate: " + action.getStatusMessage();
	}
	public void UnmuteDeviceDelegate(ActionDeviceUnmute action) {
		message = "Unmute delegate: " + action.getStatusMessage();
	}
}
