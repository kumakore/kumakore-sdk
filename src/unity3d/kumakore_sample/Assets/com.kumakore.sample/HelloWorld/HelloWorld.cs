using UnityEngine;
using System.Collections;
using com.kumakore;
using System;

public class HelloWorld : MonoBehaviour {

	private static readonly String TAG = typeof(HelloWorld).Name;	
	private const string API_KEY = "292c9e31e5187c58b58f5f8588edc92d"; //sample
	private const int DASHBOARD_VERSION = 1394062976;
	private const string APP_VERISON = "0.0";
	private const string EMAIL = "test@sinuouscode.com";
	private const string PASSWORD = "test";

	private KumakoreApp _app;

	// Use this for initialization
	private void Start() {
		_app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);

		//_app.delete();

		_app.load();
		
		//signout();

		if (_app.getUser().hasSessionId()) {
			Kumakore.LOGI(TAG, _app.getUser().getName() + " already signed in.");
			
			_app.logInfo("TEST").async(delegate(ActionAppLog action) {
				Debug.Log("TEST CALLBACK!");
			});
			Debug.Log("TEST AFTER!");
		} else {
			// could be not logged in or
			// we need to create an account			
			signup(EMAIL);
		}
	}
	
	private void Update() {
		_app.getDispatcher().dispatch();	
	}

	private void OnDestroy() {
		_app.save();
	}

	// returns true if error is handled
	private bool handleErrorCodes(KumakoreActionBase action) {
		if (action.getCode() == StatusCodes.APP_API_KEY_INVALID) {
			// developer used the wrong API key?
			//...

			return true;
		} else if (action.getCode() == StatusCodes.APP_DASHBOARD_VERSION_INVALID) {
			// dashboard version is not valid. update KumakoreApp dashboard version, then retry?
			// the server dashboard version is stored in the action
			// update the KumakoreApp dashboard version to match
			_app.setDashboardVersion (action.getDashboardVersion ());

			//retry a few times?
			if (action.getExecutions() < 3) {
				//NOTE: no need to provide callback because the action already
				// associated the callback 
				// true to reset the action state
				action.async(true); 
			}

			return true;
		} else if (action.getCode() == StatusCodes.APP_SESSION_ID_INVALID) {
			// session id is not valid. get new session id, then retry?
			//..
			return true;
		} else if (action.getCode() == StatusCodes.APP_VERSION_INVALID) {
			// app version is not valid. force users to update or not?
			//...

			return true;
		} else if (action.getCode() == StatusCodes.NETWORK_ERROR) {
			//...
			return true;
		} else if (action.getCode() == StatusCodes.PROTOCOL_ERROR) {
			//...
			return true;
		} 

		return false;
	}

	private void signup(String usernameOrEmail) {

		Kumakore.LOGI(TAG, "signup " + usernameOrEmail + " ...");
		_app.signup(usernameOrEmail).async(delegate(ActionUserSignup action) {

			if (action.getCode () == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, _app.getUser().getName() + " signed in.");
				return;
			} else if (handleErrorCodes(action)) {
				//...
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

		Kumakore.LOGI(TAG, "signin " + usernameOrEmail + " ...");
		_app.signin(usernameOrEmail, password).async(delegate(ActionUserSignin action) {

			if (action.getCode () == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, _app.getUser().getName() + " signed in.");
				platform();
				return;
			} else if (handleErrorCodes(action)) {
				//...
			} else if (action.getCode() == StatusCodes.USER_NAME_EMAIL_PASSWORD_INVALID) {
				//...
			} 

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}

	private void signout() {

		Kumakore.LOGI(TAG, "signout " + _app.getUser().getName() + " ...");
		_app.getUser().signout().async(delegate(ActionUserSignout action) {

			if (action.getCode () == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, _app.getUser().getName() + " signed out.");
				return;
			} else if (handleErrorCodes(action)) {
				//...
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}

	private void platform() {

		Kumakore.LOGI(TAG, "platform ...");
		_app.platform ().async (delegate(ActionAppPlatform action) {
			if (action.getCode () == StatusCodes.SUCCESS) {
				Kumakore.LOGI(TAG, "Current: " + action.getCurrent() + ", Mimimum: " + action.getMinimum());
			}
		});
	}
	
	private void getLeaderboards() {
		_app.getLeaderboards().get().async(delegate(ActionLeaderboardGet action) {

			if (action.getCode () == StatusCodes.SUCCESS) {
				
				foreach(Leaderboard leaderboard in _app.getLeaderboards().Values) {
					setLeaderboardScoreForCurrentUser(leaderboard, 100);
					Kumakore.LOGD(TAG, "Leaderboard:" + leaderboard.getName());
				}
				
				return;
			} else if (handleErrorCodes(action)) {
				//...
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}
	
	private void setLeaderboardScoreForCurrentUser(Leaderboard leaderboard, int score) {
					
		// updates the score for the current user for this leaderboard
		leaderboard.setUserScore(score).async(delegate(ActionLeaderboardSetScore action) {

			if (action.getCode () == StatusCodes.SUCCESS) {
				getLeaderboardMembers(leaderboard, 0, 10);
				Kumakore.LOGD(TAG, "Leaderboard:" + leaderboard.getName() + " set to " + score + " for UserId:" + _app.getUser().getId());
				return;
			} else if (handleErrorCodes(action)) {
				//...
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}
	
	private void getLeaderboardMembers(Leaderboard leaderboard, int start, int end) {
		leaderboard.getMembsGivenRange(start, end).async(delegate(ActionLeaderboardGetMembers action) {

			if (action.getCode () == StatusCodes.SUCCESS) {
				
				foreach(LeaderboardMember member in leaderboard.getMembers()) {
					Kumakore.LOGD(TAG, "LeaderboardMember:" + member.getMemberId());
				}
				
				return;
			} else if (handleErrorCodes(action)) {
				//...
			}

			Kumakore.LOGE(TAG, action.getStatusMessage());
		});
	}
}
