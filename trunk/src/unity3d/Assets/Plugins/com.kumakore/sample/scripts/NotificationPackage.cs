using System.Collections.Generic;
using System;

namespace com.kumakore.sample
{
	public class NotificationPackage
	{
		#region Fields
		private static string TAG = "com.kumakore.NotificationPackage";
		
		private static string NOTIFICAITON_PACKAGE_MESSAGE = "message";
		private static string NOTIFICAITON_PACKAGE_ICON = "icon";
		private static string NOTIFICAITON_PACKAGE_SOUND = "sound";
		private static string NOTIFICAITON_PACKAGE_KEY_VALUE_PAIR_DELIMITOR = "###,###";
		private static string NOTIFICAITON_PACKAGE_KEY_VALUE_DELIMITOR = "###:###";
		
		private Dictionary<string, string> _map = new Dictionary<string, string>();
			
		#endregion
			
		#region ctor
			
		public NotificationPackage() { }
		
		#endregion
		
		#region members
		
		public string getstring(string key) {
			if (_map.ContainsKey(key)) { 
				return _map[key];
			} else {
				Kumakore.LOGE(TAG, " key='" + key + "' not found.");
				return "";
			}
		}
		
		public void setstring(string key, string value) {
			_map[key] = value;		
		}
		
		public bool hasValue(string key) {
			return _map.ContainsKey(key);
		}		
		
		public string getMessage() {
			return getstring(NOTIFICAITON_PACKAGE_MESSAGE);
		}
		
		public void setMessage(string value) {
			setstring(NOTIFICAITON_PACKAGE_MESSAGE, value);
		}
		
		public string getIcon() {
			return getstring(NOTIFICAITON_PACKAGE_ICON);
		}
		
		public void setIcon(string value) {
			setstring(NOTIFICAITON_PACKAGE_ICON, value);
		}
		
		public string getSound() {
			return getstring(NOTIFICAITON_PACKAGE_SOUND);
		}
		
		public void setSound(string value) {
			setstring(NOTIFICAITON_PACKAGE_SOUND, value);
		}
		
		public string serialize() {
			string data = string.Empty;
			foreach(string key in _map.Keys) {
				string value = _map[key];				
				if (!string.IsNullOrEmpty(data)) data += NOTIFICAITON_PACKAGE_KEY_VALUE_PAIR_DELIMITOR;
				data += key + NOTIFICAITON_PACKAGE_KEY_VALUE_DELIMITOR + value;
			}
			return data;
		}
		
		public void deserialize(string data) {
			if (!string.IsNullOrEmpty(data)) {
				string[] keyValuePairs = data.Split(new string[] { NOTIFICAITON_PACKAGE_KEY_VALUE_PAIR_DELIMITOR }, StringSplitOptions.None);	
				foreach(string keyValuePair in keyValuePairs) {
					string[] keyValue = keyValuePair.Split(new string[] { NOTIFICAITON_PACKAGE_KEY_VALUE_DELIMITOR }, StringSplitOptions.None);
					if (keyValue.Length == 2) {					
						setstring(keyValue[0], keyValue[1]);
					}
				}
			}
		}
		
		public override String ToString() {
			return serialize();	
		}
		
		#endregion
	}
}

