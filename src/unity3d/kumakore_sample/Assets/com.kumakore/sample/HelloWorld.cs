using UnityEngine;
using System.Collections;
using com.kumakore;
using System;

public class HelloWorld : MonoBehaviour {
	
	const string API_KEY = "292c9e31e5187c58b58f5f8588edc92d"; //sample
	const int DASHBOARD_VERSION = 1375571696;
	const string APP_VERISON = "0.0";
	const string EMAIL = "test@sinuouscode.com";
	const string PASSWORD = "test";
	
	KumakoreApp _app;
	
	// Use this for initialization
	void Start () {
		_app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);
		
		//signout();
		
		if(_app.user().hasSessionId()) {
			print (_app.user().getName() + " already signed in.");		
		} else {
			// could be not logged in or
			// we need to create an account			
			signup(EMAIL);
		}
	}
	
	private void signup(String usernameOrEmail) {
			
		print("signup -> " + usernameOrEmail);
		_app.signup(usernameOrEmail).sync (delegate(ActionAppSignup action) {
			StatusCodes status = action.getStatusCode();
			String statusMsg = action.getStatusMessage();
			
			if (status == StatusCodes.SUCCESS) {
				print (_app.user().getName() + " signed in.");
				return;
			} else if (status == StatusCodes.USER_SIGNUP_ERROR) {
				if (statusMsg.Contains("name: is already taken")) {
					signin(usernameOrEmail, PASSWORD);
					return;
				} else if (statusMsg.Contains("email: is already taken")) {
					signin(usernameOrEmail, PASSWORD);
					return;
				} else if (statusMsg.Contains("email: is invalid")) {
					// ...					
				} else if (statusMsg.Contains("name: is invalid")) {
					// ...					
				} else {
					// ...					
				}				
			} 
			
			Debug.LogError (statusMsg);	
		});		
	}
	
	private void signin(String usernameOrEmail, String password) {
		
		print("signin -> " + usernameOrEmail);
		_app.signin(usernameOrEmail, password).sync(delegate(ActionAppSignin action) {
			StatusCodes status = action.getStatusCode();
			String statusMsg = action.getStatusMessage();
			
			if (status == StatusCodes.SUCCESS) {
				print (_app.user().getName() + " signed in.");
				return;
			} else if (status == StatusCodes.USER_SIGNIN_ERROR) {
				if (statusMsg.Contains("No user match")) {
					statusMsg = "Username/Email or Password did not match.";
				} else {
					
				}				
			} 
			
			Debug.LogError (statusMsg);	
		});
	}
	
	private void signout() {
		
		print("signout -> " + _app.user().getName());
		_app.user().signout().sync(delegate(ActionUserSignout action) {
			StatusCodes status = action.getStatusCode();
			String statusMsg = action.getStatusMessage();
			
			if (status == StatusCodes.SUCCESS) {
				print (_app.user().getName() + " signed out.");
				return;
			} 
			
			Debug.LogError (statusMsg);	
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
