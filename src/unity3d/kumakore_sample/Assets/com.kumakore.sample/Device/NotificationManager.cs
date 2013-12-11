using System;
using UnityEngine;

namespace com.kumakore
{
	public class NotificationManager : NotificationReceiver
	{
		private static readonly string TAG = typeof(NotificationManager).Name;	
		
		private const string API_KEY = "efc3993b2afd05a99f978342604046bc"; //sample
		private const int DASHBOARD_VERSION = 1375571696;
		private const string APP_VERISON = "0.0";
		private const string EMAIL = "test@sinuouscode.com";
		private const string PASSWORD = "test";
		private const string SENDER_ID = "119922458786";
		private string _info = "";
		
		public NotificationManager ()
		{		
		}
		
		private KumakoreApp _app;
	
		// Use this for initialization
		private void Awake() {
			_app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);
	
			//_app.delete();
	
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
		
		void OnGUI ()
		{	
		    GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		    boxStyle.fontSize = 30;
			
			if (_app.user().hasSessionId()) {
				
				GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
				GUI.Box(new Rect(0,0, Screen.width, 40), "Notification Manager", boxStyle);
				
			    GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
			    btnStyle.fontSize = 40;
				
				if (GUI.Button(new Rect(0, 100,Screen.width, 100), "register", btnStyle)) {
					register(SENDER_ID);
				}
				
				if (GUI.Button(new Rect(0, 225,Screen.width,100), "unregister", btnStyle)) {
					unregister();
				}
	
			    GUIStyle lblStyle = new GUIStyle(GUI.skin.label);
			    lblStyle.fontSize = 40;
				if (!string.IsNullOrEmpty(_info))
					GUI.Label(new Rect(0, 600, Screen.width, 500), _info, lblStyle);
				
				GUI.EndGroup();
			} else {
				GUI.Box(new Rect(0,0, Screen.width, 40), "no valid Session Id; See log", boxStyle);
			}
		}
		
		protected override void onNotify (NotificationPackage pack)
		{			
			_info = pack.getMessage();
			Debug.Log(pack.getMessage());	
		}
		
		public void onRegister (string token)
		{
			_info = token;
			Debug.Log(token);						
		}
		
		public void onError (string msg)
		{
			_info = msg;
			Debug.LogError(msg);			
		}
		
		private void register(string senderIds) {
			_app.user().device().register(senderIds).sync(delegate(ActionDeviceRegister action) {
	
				if (action.getCode() == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, "registered: " + action.getToken());
					_info = "registered: " + action.getToken();
					return;
				} 
	
				Kumakore.LOGE(TAG, action.getStatusMessage());
				_info = action.getStatusMessage();
			});
		}
		
		private void unregister() {
			_app.user().device().unregister().sync(delegate(ActionDeviceUnregister action) {
	
				if (action.getCode() == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, "unregistered");
					_info = "unregistered";
					return;
				} 
	
				Kumakore.LOGE(TAG, action.getStatusMessage());
				_info = action.getStatusMessage();
			});
		}
		
		private void signup(String usernameOrEmail) {
	
			Kumakore.LOGI(TAG, "signup -> " + usernameOrEmail);
			_app.signup(usernameOrEmail).sync(delegate(ActionAppSignup action) {
	
				if (action.getCode() == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, _app.user().getName() + " signed in.");
					return;
				} else if (action.getCode() == StatusCodes.USER_NAME_TAKEN) {
					signin(usernameOrEmail, PASSWORD);
					return;
				} else if (action.getCode() == StatusCodes.USER_EMAIL_TAKEN) {
					signin(usernameOrEmail, PASSWORD);
					return;
				} else if (action.getCode() == StatusCodes.USER_EMAIL_INVALID) {
					// ...					
				} else if (action.getCode() == StatusCodes.USER_NAME_INVALID) {
					// ...					
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
				} else if (action.getCode() == StatusCodes.USER_NAME_EMAIL_PASSWORD_INVALID) {
	
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
}

