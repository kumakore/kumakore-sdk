using UnityEngine;
using System.Collections;
using com.kumakore;

public class AppActionsScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	private string fbToken = "CAAGJJ0L1ZC0cBAKM7qyqICJLwpExK2KU8m4nZBGCLbUZBVYexfenZCGk1cA2D6nujBenXdX4fJG7E2imo7nJGyE4zeF3psMUiQ0HuSek7hlo7ZAXWZAdw0gzpmxX0ajpIRf4nbNtzVAloBovZAP8xFX6G7yZC9OJsGA4ejcI6leFQPqrHbBIZBSUN";
	private string logMessage = "Log message";
	
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI () {
		// User sign in GUI
		if(kumakore.user ().hasSessionId()) {
			GUI.Label(new Rect(10,10,400,60),"Signed in");
			// Facebook actions
			if(GUI.Button(new Rect(10,90,200,60),"Facebook login")) kumakore.facebookLogin(fbToken).async (FacebookLoginDelegate);
			if(GUI.Button(new Rect(10,170,200,60),"Connect Facebook")) kumakore.facebookConnectAccount(fbToken).async (FacebookConnectDelegate);
			if(GUI.Button(new Rect(10,250,200,60),"Facebook Remove")) kumakore.facebookRemoveAccount().async (FacebookRemoveDelegate);
			// Log actions
			logMessage = GUI.TextField(new Rect(240,90,200,60),logMessage);
			if(GUI.Button(new Rect(240,170,200,60),"Log Info")) kumakore.logInfo(logMessage).async (LogDelegate);
			if(GUI.Button(new Rect(240,250,200,60),"Log Debug")) kumakore.logDebug(logMessage).async (LogDelegate);
			if(GUI.Button(new Rect(240,330,200,60),"Log Warning")) kumakore.logWarning(logMessage).async (LogDelegate);
			if(GUI.Button(new Rect(240,410,200,60),"Log Error")) kumakore.logError(logMessage).async (LogDelegate);
			if(GUI.Button(new Rect(240,490,200,60),"Log Critical")) kumakore.logCritical(logMessage).async (LogDelegate);
		} else {
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(450,110,200,60),"Reset password")) kumakore.passwordReset(useremail).async (PasswordResetDelegate);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) kumakore.signin (useremail,password).async (SigninDelegate);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	
	// DELEGATES
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
	}
	public void PasswordResetDelegate(ActionAppUserPasswordReset action) {
		message = "Password reset delegate: " + action.getStatusMessage();
	}
	public void FacebookLoginDelegate(ActionFacebookLogin action) {
		message = "Facebook Login delegate: " + action.getStatusMessage ();
	}
	public void FacebookConnectDelegate(ActionFacebookConnectAccount action) {
		message = "Facebook Connect delegate: " + action.getStatusMessage ();
	}
	public void FacebookRemoveDelegate(ActionFacebookRemoveAccount action) {
		message = "Facebook Remove delegate: " + action.getStatusMessage ();
	}
	public void LogDelegate(ActionAppLog action) {
		message = "Log delegate: " + action.getCode();
	}
}
