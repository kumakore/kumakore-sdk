using UnityEngine;
using UnityEditor;

namespace com.kumakore
{
	public static class NotificationEditorHelpers
	{
#pragma warning disable 0219
        [MenuItem("Component/KumaKore/Notification Manager")]
		[MenuItem("KumaKore/Notification Manager")]
		public static void AddToSelections()
		{
		   foreach(GameObject go in Selection.gameObjects){
//	           NotificationManager obj = go.GetComponent<NotificationManager>();
//	           if (obj == null) obj = go.AddComponent<NotificationManager>();
		   }
		}
	}
}

