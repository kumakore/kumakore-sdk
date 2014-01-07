using UnityEngine;
using System.Collections;
using com.kumakore;

public class SignupScene : MonoBehaviour {
	#region demo variables
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string username = "username";
	private string newpassword = "password";
	private string message;
	#endregion
	
	#region Kumakore Calls
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
		kumakore.load ();
	}
	
	void OnDestroy() {
		kumakore.save();
	}
	
	// Kumakore calls
	public void Signup(string user, string name) {
		kumakore.signup (user).async (delegate(ActionUserSignup action) {
			message = "Signup delegate: " + action.getStatusMessage();
			if(action.getCode() == StatusCodes.SUCCESS && !string.IsNullOrEmpty(username)) {
				if(!string.IsNullOrEmpty(name)) kumakore.getUser().update().setName(name).async();
			}
		});
	}
	public void UserUpdate(string name, string email, string pass) {
		kumakore.getUser ().update (name,email,pass).async (delegate(ActionUserUpdate action) {
			if(action.getCode() == StatusCodes.SUCCESS)
				message = "Update user delegate: " + kumakore.getUser ().getName () + ", " + kumakore.getUser ().getEmail();
			else message = "Update user delegate: " + action.getStatusMessage();
		});
	}
	public void GetUserId(string user) {
		kumakore.getUserId(user).async (delegate(ActionUserGetUserId action) {
			if(action.getCode() == StatusCodes.SUCCESS) {
				message = "Get User Id delegate: " + action.getUserId();
			} else {
				message = "Get User Id delegate: " + action.getStatusMessage();
			}
		});
	}
	#endregion
	
	#region GUI
	
	void OnGUI () {
		if(kumakore.getUser ().hasSessionId()) {
			GUI.Label (new Rect(10,20,400,60),"Continue with sign up providing the following info");
			GUI.Box (new Rect(5,80,415,345),"Update user info");
			username = GUI.TextField (new Rect(10,115,400,60),username);
			useremail = GUI.TextField (new Rect(10,195,400,60),useremail);
			newpassword = GUI.TextField (new Rect(10,275,400,60),newpassword);
			if(GUI.Button (new Rect(10,355,400,60),"Update")) UserUpdate(username,useremail,newpassword);
		} else {
			// User sign up GUI
			GUI.Label (new Rect(10,10,350,60),"Enter your email or username to sign up");
			useremail = GUI.TextField (new Rect(10,90,400,60),useremail);
			username = GUI.TextField(new Rect(450,90,200,60),username);
			if(GUI.Button (new Rect(10,170,400,60),"Sign up")) Signup (useremail,username);
			if(GUI.Button (new Rect(450,170,200,60),"Get User ID")) GetUserId (username);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	#endregion
}
