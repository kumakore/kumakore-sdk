using UnityEngine;
using System.Collections;
using com.kumakore;

namespace com.kumakore.sample {

	public partial class AppBehav : SigninBehav {
		
		private static readonly string TAG = typeof(AppBehav).Name;	

		private string fbToken = "CAAGJJ0L1ZC0cBAKM7qyqICJLwpExK2KU8m4nZBGCLbUZBVYexfenZCGk1cA2D6nujBenXdX4fJG7E2imo7nJGyE4zeF3psMUiQ0HuSek7hlo7ZAXWZAdw0gzpmxX0ajpIRf4nbNtzVAloBovZAP8xFX6G7yZC9OJsGA4ejcI6leFQPqrHbBIZBSUN";
		private string logMessage = "test logger";

		public void passwordReset(string user) {
			app.passwordReset(user).async (delegate(ActionUserPasswordReset action) {
				Kumakore.LOGI (TAG, "Password reset callback: " + action.getCode ());
			});
		}

		public void facebookLogin(string token) {
			app.facebookLogin(fbToken).async (delegate(ActionFacebookSignin action) {
				Kumakore.LOGI (TAG, "facebook login callback: " + action.getCode ());
			});
		}

		public void facebookConnect(string token) {
			app.facebookConnect(fbToken).async (delegate(ActionFacebookConnect action) {
				Kumakore.LOGI (TAG, "facebook connect callback: " + action.getCode ());
			});
		}

		public void facebookRemove() {
			app.facebookDeauthorize().async (delegate(ActionFacebookDeauthorize action) {
				Kumakore.LOGI (TAG, "facebook remove callback: " + action.getCode ());
			});
		}

		public void logInfo(string message) {
			app.logInfo(message).async (logCallback);
		}

		public void logDebug(string message) {
			app.logDebug(message).async (logCallback);
		}

		public void logWarning(string message) {
			app.logWarning(message).async (logCallback);
		}

		public void logError(string message) {
			app.logError(message).async (logCallback);
		}

		public void logCritical(string message) {
			app.logCritical(message).async (logCallback);
		}

		private void logCallback(ActionAppLog action) {
			Kumakore.LOGI (TAG, "Log callback: " + action.getCode ());
		}
	}
}