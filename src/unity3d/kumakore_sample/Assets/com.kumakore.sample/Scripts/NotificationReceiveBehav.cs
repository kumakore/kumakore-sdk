using UnityEngine;
using System;
using System.Collections.Generic;

namespace com.kumakore.sample
{
	public abstract class NotificationReceiveBehav : SigninBehav
	{			
		#region fields
		
		// base64("com.kumakore.Notification")
		// DO NOT RENAME/CHANGE INERFACE: used for callbacks from android kumakore API
		private static string UNITY_MESSAGE_OBJECT_NAME = "Y29tLmt1bWFrb3JlLk5vdGlmaWNhdGlvbg==";
		
		#endregion
		
		#region ctor/dtor
		
		public static NotificationReceiveBehav Create() {
			GameObject obj = new GameObject(UNITY_MESSAGE_OBJECT_NAME);
			obj.hideFlags = HideFlags.HideInHierarchy;			
			return obj.AddComponent<NotificationReceiveBehav>();
		}
		
		#endregion		
		
		#region Events

		// Fired when the notification manager has received a push notification
		public event Action<NotificationPackage> onNotifyEvent;

		private void RaiseOnNotify(NotificationPackage pack) {
			//threadsafe
			Action<NotificationPackage> temp = onNotifyEvent;
			if (temp != null)
				temp(pack);
			
			onNotify(pack);
		}
		
		#endregion 
		
		#region
		
		// DO NOT RENAME/CHANGE INERFACE: callbacks from android kumakore API
		private void onMessage(string packageData) {
			NotificationPackage pack = new NotificationPackage();
			pack.deserialize(packageData);			
			
			RaiseOnNotify(pack);
		}
		
		protected abstract void onNotify(NotificationPackage package);
		
		#endregion
	}
}

