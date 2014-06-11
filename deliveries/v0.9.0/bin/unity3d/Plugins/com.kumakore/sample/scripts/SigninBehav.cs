using UnityEngine;
using System.Collections;
using com.kumakore;
using System;
using com.kumakore.plugin.util;

namespace com.kumakore.sample {

	public partial class SigninBehav : KumakoreBehav {

		private static readonly string TAG = typeof(SigninBehav).Name;

		private static readonly string DEFAULT_USERNAME = NamingHelper.generateUserName();
		private static readonly string DEFAULT_EMAIL = DEFAULT_USERNAME + "@domain.com";
		private static readonly string DEFAULT_PASSWORD = NamingHelper.generatePassword();

		public string userName = DEFAULT_USERNAME;
		public string userEmail = DEFAULT_EMAIL;
		public string userPassword = DEFAULT_PASSWORD;

		protected virtual void Start() {

			if (app.getUser().hasSessionId()) {
				Kumakore.LOGI(TAG, app.getUser().getName() + " already signed in.");
			} else {
				// could be not logged in or
				// we need to create an account			
				signup(userEmail);
			}
		}

		public void signup(String usernameOrEmail) {

			Kumakore.LOGI(TAG, "signup " + usernameOrEmail + " ...");
			app.signup(usernameOrEmail).async(delegate(ActionUserSignup action) {

				if (action.getCode () == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, app.getUser().getName() + " signed in.");
					updateUser(userName, userEmail, userPassword);
					return;
				} else if (handleErrorCodes(action)) {
					//...
				} else if (action.getCode() == StatusCodes.USER_NAME_TAKEN) {
					Kumakore.LOGI(TAG, action.getStatusMessage());
					signin(usernameOrEmail, userPassword);
					return;
				} else if (action.getCode() == StatusCodes.USER_EMAIL_TAKEN) {
					Kumakore.LOGI(TAG, action.getStatusMessage());
					signin(usernameOrEmail, userPassword);
					return;
				} else if (action.getCode() == StatusCodes.USER_EMAIL_INVALID) {
					// ...					
				} else if (action.getCode() == StatusCodes.USER_NAME_INVALID) {
					// ...					
				}

				Kumakore.LOGE(TAG, action.getStatusMessage());
			});
		}

		public void signin(String usernameOrEmail, String password) { 

			Kumakore.LOGI(TAG, "signin " + usernameOrEmail + " ...");
			app.signin(usernameOrEmail, password).async(delegate(ActionUserSignin action) {

				if (action.getCode () == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, app.getUser().getName() + " signed in.");
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

		public void signout() {

			Kumakore.LOGI(TAG, "signout " + app.getUser().getName() + " ...");
			app.getUser().signout().async(delegate(ActionUserSignout action) {

				if (action.getCode () == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, app.getUser().getName() + " signed out.");
					return;
				} else if (handleErrorCodes(action)) {
					//...
				}

				Kumakore.LOGE(TAG, action.getStatusMessage());
			});
		}
		
		public void updateUser(string name, string email, string password) {

			Kumakore.LOGI(TAG, "update User " + app.getUser().getName() + " ...");
			app.getUser ().update (name, email, password).async (delegate(ActionUserUpdate action) {
				
				if (action.getCode () == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, app.getUser().getName() + " user updated.");
					return;
				} else if (handleErrorCodes(action)) {
					//...
				}
				
				Kumakore.LOGE(TAG, action.getStatusMessage());
			});
		}

		public void platform() {

			Kumakore.LOGI(TAG, "platform ...");
			app.platform ().async (delegate(ActionAppPlatform action) {
				if (action.getCode () == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, "Current: " + action.getCurrent() + ", Mimimum: " + action.getMinimum());
				}
			});
		}
		
		public void getLeaderboards() {
			app.getLeaderboards().get().async(delegate(ActionLeaderboardGet action) {

				if (action.getCode () == StatusCodes.SUCCESS) {
					
					foreach(Leaderboard leaderboard in app.getLeaderboards().Values) {
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
		
		public void setLeaderboardScoreForCurrentUser(Leaderboard leaderboard, int score) {
						
			// updates the score for the current user for this leaderboard
			leaderboard.setUserScore(score).async(delegate(ActionLeaderboardSetScore action) {

				if (action.getCode () == StatusCodes.SUCCESS) {
					getLeaderboardMembers(leaderboard, 0, 10);
					Kumakore.LOGD(TAG, "Leaderboard:" + leaderboard.getName() + " set to " + score + " for UserId:" + app.getUser().getId());
					return;
				} else if (handleErrorCodes(action)) {
					//...
				}

				Kumakore.LOGE(TAG, action.getStatusMessage());
			});
		}
		
		public void getLeaderboardMembers(Leaderboard leaderboard, int start, int end) {
			leaderboard.getMembsGivenRange(start, end).async(delegate(ActionLeaderboardGetMembers action) {

				if (action.getCode () == StatusCodes.SUCCESS) {
					
					foreach(LeaderboardMember member in leaderboard.getMembers()) {
						Kumakore.LOGD(TAG, "LeaderboardMember:" + member.getUserName());
					}
					
					return;
				} else if (handleErrorCodes(action)) {
					//...
				}

				Kumakore.LOGE(TAG, action.getStatusMessage());
			});
		}
	}
}