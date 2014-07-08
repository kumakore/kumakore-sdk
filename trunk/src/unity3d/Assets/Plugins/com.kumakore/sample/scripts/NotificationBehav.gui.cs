using System;
using UnityEngine;

namespace com.kumakore.sample
{
	public partial class NotificationBehav : NotificationReceiveBehav
	{

	    protected override void OnGUI ()
		{	
		    GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		    boxStyle.fontSize = 30;
			
			if (app.getUser().hasSessionId()) {
				
				GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
				GUI.Box(new Rect(0,0, Screen.width, 40), "Notification Manager", boxStyle);
				
			    GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
			    btnStyle.fontSize = 40;
				
				if (GUI.Button(new Rect(0, 100,Screen.width, 100), "register", btnStyle)) {
					register(SENDER_ID);
				}
				
				if (GUI.Button(new Rect(0, 225,Screen.width,100), "unregister", btnStyle)) {
					unregister();
				}
				
				if (GUI.Button(new Rect(0, 350,Screen.width,100), getEnabled() ? "disable" : "enable", btnStyle)) {
					//toggle
					setEnabled(!getEnabled());
       			}
	
			    GUIStyle lblStyle = new GUIStyle(GUI.skin.label);
			    lblStyle.fontSize = 40;
				if (!string.IsNullOrEmpty(_info))
					GUI.Label(new Rect(0, 600, Screen.width, 500), _info, lblStyle);
				
				GUI.EndGroup();
			} else {
				GUI.Box(new Rect(0,0, Screen.width, 40), "no valid Session Id; See log", boxStyle);
			}
		}
	}
}

