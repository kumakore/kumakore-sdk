using System;
using UnityEngine;

#if UNITY_ANDROID
using com.kumakore.plugin.android.push;
#endif

namespace com.kumakore.sample
{
	public partial class NotificationBehav : NotificationReceiveBehav
	{
		private static readonly string TAG = typeof(NotificationBehav).Name;	

		private static readonly string SENDER_ID = "119922458786";

		private string _info = "";

		public void setEnabled(bool enabled) {
			// required, otherwise notifications will be dropped
			// use this to dynamically turn on and off
			// Notifications instead of unregistering
#if UNITY_ANDROID
			new CmdPushEnabledStatus (enabled).sync ();
#endif
		}
		
		public bool getEnabled() {
#if UNITY_ANDROID
			bool ret = false;
			new CmdPushEnabledStatus().sync (delegate(CmdPushEnabledStatus cmd) {
				if (cmd.getCode() == CmdPushEnabledStatus.StatusCodes.Success) {
					ret = cmd.getEnabled ();
				}
			});
			return ret;
#else
			return false;
#endif
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

		private void register(string senderId) {
#if UNITY_ANDROID
			new CmdRegisterSender (senderId, app.getDispatcher ()).async (delegate(CmdRegisterSender cmd) {
				if (cmd.getCode() == CmdRegisterSender.StatusCodes.Success) {
					registerToken(cmd.getToken());
					return;
				}
				
				Kumakore.LOGE(TAG, cmd.getStatusMessage());
			});
#endif
		}

		private void registerToken(string token) {
			
			app.getUser().device().register(token).sync(delegate(ActionDeviceRegister action) {
				
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
			app.getUser().device().unregister().sync(delegate(ActionDeviceUnregister action) {
	
				if (action.getCode() == StatusCodes.SUCCESS) {
					Kumakore.LOGI(TAG, "unregistered");
					_info = "unregistered";
					return;
				} 
	
				Kumakore.LOGE(TAG, action.getStatusMessage());
				_info = action.getStatusMessage();
			});
		}
	}
}

