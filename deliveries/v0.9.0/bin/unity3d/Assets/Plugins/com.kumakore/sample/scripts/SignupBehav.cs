using UnityEngine;
using System.Collections;
using com.kumakore;

namespace com.kumakore.sample {
	public partial class SignupBehav : SigninBehav {
		
		private static readonly string TAG = typeof(SignupBehav).Name;	

		protected override void Start() {
			
			if (app.getUser().hasSessionId()) {
				Kumakore.LOGI(TAG, app.getUser().getName() + " already signed in.");
			} 
		}

		// app calls
		public void signup(string user, string name) {
			app.signup (user).async (delegate(ActionUserSignup action) {
				Kumakore.LOGI (TAG, "signup callback: " + action.getCode ());
				if(action.getCode() == StatusCodes.SUCCESS && !string.IsNullOrEmpty(name)) {
					if(!string.IsNullOrEmpty(name)) 
						app.getUser().update().setName(name).async();
				}
			});
		}

		public void userUpdate(string name, string email, string pass) {
			app.getUser ().update (name,email,pass).async (delegate(ActionUserUpdate action) {
				if(action.getCode() == StatusCodes.SUCCESS)
					Kumakore.LOGI (TAG, "Update user callback: " + app.getUser ().getName () + ", " + app.getUser ().getEmail());
				else 
					Kumakore.LOGI (TAG, "Update user callback: " + action.getStatusMessage());
			});
		}

		public void getUserId(string user) {
			app.getUserId(user).async (delegate(ActionUserGetUserId action) {
				if(action.getCode() == StatusCodes.SUCCESS) 
					Kumakore.LOGI (TAG, "get user id callback: " + action.getUserId());
				else 
					Kumakore.LOGI (TAG, "get user id callback: " + action.getStatusMessage());
			});
		}
	}
}