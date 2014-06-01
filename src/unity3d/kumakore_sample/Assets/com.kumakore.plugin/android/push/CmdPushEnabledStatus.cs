#if UNITY_ANDROID
using System;
using System.Collections;
using UnityEngine;

namespace com.kumakore.plugin.android.push {

	/// <summary>
	/// Notification command used to get a push token for android
	/// </summary>
	public class CmdPushEnabledStatus : Command<CmdPushEnabledStatus.IPlugin, CmdPushEnabledStatus.StatusCodes> {
		private static readonly String TAG = typeof(CmdPushEnabledStatus).Name;

		public enum StatusCodes
		{
			Unknown = int.MaxValue,
			Success = 0,
			Failure = 1,
		}

		public delegate void IPlugin(CmdPushEnabledStatus cmd);

		private static AndroidJavaClass _notificationClass;
		private static AndroidJavaObject _notificationObj;
		private static AndroidJavaObject _currentActivity;

		private bool? _enabled = null;

		static CmdPushEnabledStatus() {
			
			if (Application.platform != RuntimePlatform.Android) {
				Kumakore.LOGW (TAG, "Android Running in editor mode");		
				return;
			}

			try
			{
				AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

				_currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity"); 

				_notificationClass = new AndroidJavaClass("com.kumakore.Notification");
					
				_notificationObj = _notificationClass.CallStatic<AndroidJavaObject>("getInstance");
			} catch (Exception ex) {
				String error = "Failed to initialize Notification; " + ex.Message;
				Kumakore.LOGE (TAG, error);		
				throw new InvalidOperationException(error);
			}
		}

		public CmdPushEnabledStatus(IInvokable dispatcher = null) : base(StatusCodes.Unknown, dispatcher) {
			_enabled = null;
		}

		public CmdPushEnabledStatus(bool enabled, IInvokable dispatcher = null) : base(StatusCodes.Unknown, dispatcher) {
			_enabled = enabled;
		}

		public bool getEnabled() {
			return _enabled.HasValue ? _enabled.Value : false;
		}

		public CmdPushEnabledStatus setEnabled(bool enabled) {
			_enabled = enabled;
			return this;
		}

		protected override StatusCodes onExecute ()
		{
			if (Application.platform != RuntimePlatform.Android) {
				Kumakore.LOGW (TAG, "Android Running in editor mode");		
				return StatusCodes.Failure;
			}

			StatusCodes code = (StatusCodes)_notificationObj.Call<int>("init", _currentActivity, true);

			if (code == StatusCodes.Success) {
				if (_enabled.HasValue)
					_notificationObj.Call ("setEnabled", _enabled.Value ? 1 : 0);

				_enabled = _notificationObj.Call<bool> ("getEnabled");
			} else 
				code = StatusCodes.Failure;

			return code;
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
			case StatusCodes.Failure:
				return "Failure";
			}

			return base.getStatusMessage ();
		}
	}
}

#endif
