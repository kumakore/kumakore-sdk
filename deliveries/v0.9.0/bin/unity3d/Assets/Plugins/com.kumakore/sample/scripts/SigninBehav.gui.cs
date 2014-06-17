using UnityEngine;
using System.Collections;
using com.kumakore;
using System;

namespace com.kumakore.sample {

	public partial class SigninBehav : KumakoreBehav {

		protected virtual void OnGUI () {
			if(!app.getUser ().hasSessionId()) {
				GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
				userEmail = GUI.TextField (new Rect(10,30,400,60),userEmail);
				userPassword = GUI.TextField (new Rect(10,110,400,60),userPassword);

				if(GUI.Button (new Rect(10,190,400,60),"Sign in")) 
					signin (userEmail, userPassword);
			}
		}
	}
}