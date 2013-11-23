using UnityEngine;
using System.Collections;
using com.kumakore;
using System;

public class HelloWorld : MonoBehaviour {

	private static readonly String TAG = typeof(HelloWorld).Name;
	private const string API_KEY = "292c9e31e5187c58b58f5f8588edc92d"; //sample
	private const int DASHBOARD_VERSION = 1375571696;
	private const string APP_VERISON = "0.0";
	private const string EMAIL = "test@sinuouscode.com";
	private const string PASSWORD = "test";

	private KumakoreApp _app;

	// Use this for initialization
	private void Start() {
		_app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);

		_app.delete();

		_app.load();
		
		//signout();

		if (_app.user().hasSessionId()) {
			Kumakore.LOGI(TAG, _app.user().getName() + " already signed in.");
		} else {
			// could be not logged in or
			// we need to create an account			
			signup(EMAIL);
		}
	}

	private void OnDestroy() {
		_app.save();
	}

	private void signup(String usernameOrEmail) {

		Kumakore.LOGI(TAG, "signup -> " + usernameOrEmail);
		_app.signup(usernameOrEmail).sync(delegate(ActionAppSignup action) {

			if (action.getCode() == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, _app.user().getName() + " signed in.");
				return;
			} else if (action.getCode() == StatusCodes.USER_SIGNUP_ERROR) {
				if (action.hasFlags(StatusFlags.USER_NAME_TAKEN)) {
					signin(usernameOrEmail, PASSWORD);
					return;
				} else if (action.hasFlags(StatusFlags.USER_EMAIL_TAKEN)) {
					signin(usernameOrEmail, PASSWORD);
					return;
				} else if (action.hasFlags(StatusFlags.USER_EMAIL_INVALID)) {
					// ...					
				} else if (action.hasFlags(StatusFlags.USER_NAME_INVALID)) {
					// ...					
				}
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}

	private void signin(String usernameOrEmail, String password) {

		Kumakore.LOGI(TAG, "signin -> " + usernameOrEmail);
		_app.signin(usernameOrEmail, password).sync(delegate(ActionAppSignin action) {

			if (action.getCode() == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, _app.user().getName() + " signed in.");
				return;
			} else if (action.getCode() == StatusCodes.USER_SIGNIN_ERROR) {
				if (action.hasFlags(StatusFlags.USER_SIGNIN_NAME_EMAIL_PASSWORD_INVALID)) {
					// ..
				} 
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}

	private void signout() {

		Kumakore.LOGI(TAG, "signout -> " + _app.user().getName());
		_app.user().signout().sync(delegate(ActionUserSignout action) {

			if (action.getCode() == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, _app.user().getName() + " signed out.");
				return;
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}
}
