#if UNITY_ANDROID 
using System;
using System.Collections;
using UnityEngine;
using com.kumakore.plugin.util;

namespace com.kumakore.plugin.android.push {

	/// <summary>
	/// Notification command used to get a push token for android
	/// </summary>
	public class CmdRegisterSender : Command<CmdRegisterSender.IPlugin, CmdRegisterSender.StatusCodes> {
		private static readonly String TAG = typeof(CmdRegisterSender).Name;

		public enum StatusCodes
		{
			Unknown = int.MaxValue,
			Success = 0,
			ServiceMissing = 1,
			ServiceUpdateVersionRequired = 2,
			ServiceDisabled = 3,
			ServiceNotReady = 4 // java plugin error
		}

		public delegate void IPlugin(CmdRegisterSender cmd);

		private string _senderId = String.Empty;
		private string _token = String.Empty;

		public CmdRegisterSender(String senderId, IInvokable dispatcher = null) : base(StatusCodes.Unknown, dispatcher) {
			_senderId = senderId;
		}

		public string getSenderId() {
			return _senderId;
		}

		public CmdRegisterSender setSenderId(string senderId) {
			_senderId = senderId;
			return this;
		}
		
		public string getToken() {
			return _token;
		}

		protected override StatusCodes onExecute ()
		{	
			if (Application.platform != RuntimePlatform.Android) {
				Kumakore.LOGW (TAG, "Android Running in editor mode");		
				return StatusCodes.ServiceNotReady;
			}

			try
			{
				AndroidJNI.AttachCurrentThread();
				
				AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				
				AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity"); 
				
				AndroidJavaClass notificationClass = new AndroidJavaClass("com.kumakore.Notification");
				
				AndroidJavaObject notificationObj = notificationClass.CallStatic<AndroidJavaObject>("getInstance");

				setCode ((StatusCodes)notificationObj.Call<int>("init", currentActivity, true));
				
				if (getCode() == StatusCodes.Success)
					_token = notificationObj.Call<String>("register", _senderId);

			} catch (Exception ex) {
				String error = "Failed to initialize Notification; " + ex.Message;
				Kumakore.LOGE (TAG, error);		
				throw new InvalidOperationException(error);
			}

			return getCode();
		}

		protected override void onExecuted ()
		{
			base.onExecuted ();

			// if a target was provided, call the target
			if (hasTarget())
				getTarget () (this);
		}

		public override string getStatusMessage ()
		{
			switch (getCode ()) {
			case StatusCodes.Unknown:
				return "Unknown";
			case StatusCodes.Success:
				return "Success";
			case StatusCodes.ServiceMissing:
				return "Google Play Services is missing";
			case StatusCodes.ServiceUpdateVersionRequired:
				return "Google Play Services requires an update";
			case StatusCodes.ServiceDisabled:
				return "Google Play Services is disabled";
			case StatusCodes.ServiceNotReady:
				return "Google Play Services is not ready";
			}

			return base.getStatusMessage ();
		}
	}
}

#endif