using UnityEngine;
using System.Collections;
using com.kumakore;

public class AppActionsScene : MonoBehaviour {
	#region demo variables
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	private string fbToken = "CAAGJJ0L1ZC0cBAKM7qyqICJLwpExK2KU8m4nZBGCLbUZBVYexfenZCGk1cA2D6nujBenXdX4fJG7E2imo7nJGyE4zeF3psMUiQ0HuSek7hlo7ZAXWZAdw0gzpmxX0ajpIRf4nbNtzVAloBovZAP8xFX6G7yZC9OJsGA4ejcI6leFQPqrHbBIZBSUN";
	private string logMessage = "Log message";
	#endregion
	
	#region Kumakore calls
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
		kumakore.load ();
	}
	
	
	private void Update() {
		kumakore.getDispatcher().dispatch();	
	}
	
	void OnDestroy() {
		kumakore.save ();
	}
	// Kumakore calls
	public void SignIn(string user, string pass) {
		kumakore.signin (user,pass).async (delegate(ActionUserSignin action) {
			message = "Signin delegate: " + action.getStatusMessage();
		});
	}
	public void PasswordReset(string user) {
		kumakore.passwordReset(user).async (delegate(ActionUserPasswordReset action) {
			message = "Password reset delegate: " + action.getStatusMessage();
		});
	}
	public void FacebookLogin(string token) {
		kumakore.facebookLogin(fbToken).async (delegate(ActionFacebookSignin action) {
			message = "Facebook Login delegate: " + action.getStatusMessage ();
		});
	}
	public void FacebookConnect(string token) {
		kumakore.facebookConnect(fbToken).async (delegate(ActionFacebookConnect action) {
			message = "Facebook Connect delegate: " + action.getStatusMessage ();
		});
	}
	public void FacebookRemove() {
		kumakore.facebookDeauthorize().async (delegate(ActionFacebookDeauthorize action) {
			message = "Facebook Deauthorize delegate: " + action.getStatusMessage ();
		});
	}
	public void LogInfo(string message) {
		kumakore.logInfo(message).async (LogDelegate);
	}
	public void LogDebug(string message) {
		kumakore.logDebug(message).async (LogDelegate);
	}
	public void LogWarning(string message) {
		kumakore.logWarning(message).async (LogDelegate);
	}
	public void LogError(string message) {
		kumakore.logError(message).async (LogDelegate);
	}
	public void LogCritical(string message) {
		kumakore.logCritical(message).async (LogDelegate);
	}
	public void LogDelegate(ActionAppLog action) {
		message = "Log delegate: " + action.getCode();
	}
	#endregion
	
	#region GUI
	
	void OnGUI () {
		// User sign in GUI
		if(kumakore.getUser ().hasSessionId()) {
			GUI.Label(new Rect(10,10,400,60),"Signed in");
			// Facebook actions
			if(GUI.Button(new Rect(10,90,200,60),"Facebook login")) FacebookLogin (fbToken);
			if(GUI.Button(new Rect(10,170,200,60),"Connect Facebook")) FacebookConnect(fbToken);
			if(GUI.Button(new Rect(10,250,200,60),"Facebook Remove")) FacebookRemove ();
			// Log actions
			logMessage = GUI.TextField(new Rect(240,90,200,60),logMessage);
			if(GUI.Button(new Rect(240,170,200,60),"Log Info")) LogInfo (logMessage);
			if(GUI.Button(new Rect(240,250,200,60),"Log Debug")) LogDebug(logMessage);
			if(GUI.Button(new Rect(240,330,200,60),"Log Warning")) LogWarning(logMessage);
			if(GUI.Button(new Rect(240,410,200,60),"Log Error")) LogError(logMessage);
			if(GUI.Button(new Rect(240,490,200,60),"Log Critical")) LogCritical(logMessage);
		} else {
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(450,110,200,60),"Reset password")) PasswordReset (useremail);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) SignIn(useremail,password);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	#endregion
	
}
