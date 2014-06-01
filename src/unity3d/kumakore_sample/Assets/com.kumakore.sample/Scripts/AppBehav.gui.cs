using UnityEngine;
using System.Collections;
using com.kumakore;

namespace com.kumakore.sample {

	public partial class AppBehav : SigninBehav {
		
		protected override void OnGUI () {
			// User sign in GUI
			if(app.getUser ().hasSessionId()) {
				GUI.Label(new Rect(10,10,400,60),"Signed in");
				// Facebook actions
				if(GUI.Button(new Rect(10,90,200,60),"Facebook login")) facebookLogin (fbToken);
				if(GUI.Button(new Rect(10,170,200,60),"Connect Facebook")) facebookConnect(fbToken);
				if(GUI.Button(new Rect(10,250,200,60),"Facebook Remove")) facebookRemove ();
				// Log actions
				logMessage = GUI.TextField(new Rect(240,90,200,60),logMessage);
				if(GUI.Button(new Rect(240,170,200,60),"Log Info")) logInfo (logMessage);
				if(GUI.Button(new Rect(240,250,200,60),"Log Debug")) logDebug(logMessage);
				if(GUI.Button(new Rect(240,330,200,60),"Log Warning")) logWarning(logMessage);
				if(GUI.Button(new Rect(240,410,200,60),"Log Error")) logError(logMessage);
				if(GUI.Button(new Rect(240,490,200,60),"Log Critical")) logCritical(logMessage);
			} else {
				base.OnGUI();
			}
			
//			GUI.Label (new Rect(10,620,400,60),message);
			if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
		}
	}
}