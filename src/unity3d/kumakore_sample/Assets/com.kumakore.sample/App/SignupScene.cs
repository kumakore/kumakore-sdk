using UnityEngine;
using System.Collections;
using com.kumakore;

public class SignupScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string username = "username";
	private string newpassword = "password";
	private string message;
	
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI () {
		if(kumakore.user ().hasId()) {
			GUI.Label (new Rect(10,20,400,60),"Continue with sign up providing the following info");
			GUI.Box (new Rect(5,80,415,345),"Update user info");
			username = GUI.TextField (new Rect(10,115,400,60),username);
			useremail = GUI.TextField (new Rect(10,195,400,60),useremail);
			newpassword = GUI.TextField (new Rect(10,275,400,60),newpassword);
			if(GUI.Button (new Rect(10,355,400,60),"Update")) kumakore.user ().update (username,useremail,newpassword).async (UserUpdateDelegate);
		} else {
			// User sign up GUI
			GUI.Label (new Rect(10,10,350,60),"Enter your email or username to sign up");
			useremail = GUI.TextField (new Rect(10,90,400,60),useremail);
			username = GUI.TextField(new Rect(450,90,200,60),username);
			if(GUI.Button (new Rect(10,170,400,60),"Sign up")) kumakore.signup (useremail).async (SignupDelegate);
			if(GUI.Button (new Rect(450,170,200,60),"Get User ID")) kumakore.getUserId(username).async (GetUserIdDelegate);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	
	// DELEGATES
	public void SignupDelegate(ActionAppSignup action) {
		message = "Signup delegate: " + action.getStatusMessage();
		if(action.getStatusCode() == StatusCodes.SUCCESS && !string.IsNullOrEmpty(username)) {
			kumakore.user().update().setName(username).async();
		}
	}
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
	}
	public void UserUpdateDelegate(ActionUserUpdate action) {
		if(action.getStatusCode() == StatusCodes.SUCCESS)
			message = "Update user delegate: " + kumakore.user ().getName () + ", " + kumakore.user ().getEmail();
		else message = "Update user delegate: " + action.getStatusMessage();
	}
	public void GetUserIdDelegate(ActionAppGetUserId action) {
		if(action.getStatusCode() == StatusCodes.SUCCESS) {
			message = "Get User Id delegate: " + action.getUserId();
		} else {
			message = "Get User Id delegate: " + action.getStatusMessage();
		}
		
	}
}
