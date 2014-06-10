#if UNITY_ANDROID
using System;
using System.Collections;
using UnityEngine;
using com.kumakore.plugin.util;

namespace com.kumakore.plugin.android.push {

	/// <summary>
	/// PushPlugin for android
	/// </summary>
	public static class PushPlugin {
		private static readonly String TAG = typeof(PushPlugin).Name;
	
		private static IInvokable _dispatcher;

		public static void setDispatcher(IInvokable dispatcher) {
			_dispatcher = dispatcher;
		}

		public static bool getEnabled() {

			bool enabled = false;

			if (Application.platform != RuntimePlatform.Android) {
				Kumakore.LOGW (TAG, "Android Running in editor mode");		
				return false;
			}

			try
			{
				//NOTE: not caching JavaObjects
				AndroidJNI.AttachCurrentThread();
				
				AndroidJavaClass notificationClass = new AndroidJavaClass("com.kumakore.Notification"); 
				
				AndroidJavaObject notificationObj = notificationClass.CallStatic<AndroidJavaObject>("getInstance");

				enabled = notificationObj.Call<bool> ("getEnabled");
			} catch (Exception ex) {
				String error = "Failed to initialize Notification; " + ex.Message;
				Kumakore.LOGE (TAG, error);		
				throw new InvalidOperationException(error);
			}

			return enabled;
		}

		public static void setEnabled(bool enabled) {
			
			if (Application.platform != RuntimePlatform.Android) {
				Kumakore.LOGW (TAG, "Android Running in editor mode");
			}

			try
			{
				//NOTE: not caching JavaObjects
				AndroidJNI.AttachCurrentThread();
				
				AndroidJavaClass notificationClass = new AndroidJavaClass("com.kumakore.Notification");
				
				AndroidJavaObject notificationObj = notificationClass.CallStatic<AndroidJavaObject>("getInstance");
				
				notificationObj.Call ("setEnabled", enabled ? 1 : 0);
			} catch (Exception ex) {
				String error = "Failed to initialize Notification; " + ex.Message;
				Kumakore.LOGE (TAG, error);		
				throw new InvalidOperationException(error);
			}
		}

		public static CmdRegisterSender register(string senderId) {
			return new CmdRegisterSender (senderId, _dispatcher);
		}
		
		public static CmdUnregisterSender unregister() {
			return new CmdUnregisterSender (_dispatcher);
		}
	}
}

#endif
