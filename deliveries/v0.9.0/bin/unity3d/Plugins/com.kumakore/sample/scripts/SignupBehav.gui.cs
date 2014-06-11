using UnityEngine;
using System.Collections;
using com.kumakore;

namespace com.kumakore.sample {
	public partial class SignupBehav : SigninBehav {
		
		protected override void OnGUI () {
			if(app.getUser ().hasSessionId()) {
				GUI.Label (new Rect(10,20,400,60),"Continue with sign up providing the following info");
				GUI.Box (new Rect(5,80,415,345),"Update user info");
				userName = GUI.TextField (new Rect(10,115,400,60),userName);
				userEmail = GUI.TextField (new Rect(10,195,400,60),userEmail);
				userPassword = GUI.TextField (new Rect(10,275,400,60),userPassword);
				if(GUI.Button (new Rect(10,355,400,60),"Update")) 
					userUpdate(userName, userEmail, userPassword);
			} else {
				// User sign up GUI
				GUI.Label (new Rect(10,10,350,60),"Enter your email or username to sign up");
				userEmail = GUI.TextField (new Rect(10,90,400,60),userEmail);
				userName = GUI.TextField(new Rect(450,90,200,60),userName);
				if(GUI.Button (new Rect(10,170,400,60),"Sign up")) 
					signup (userEmail, userName);
				if(GUI.Button (new Rect(450,170,200,60),"Get User ID")) 
					getUserId (userName);
			}
			
//			GUI.Label (new Rect(10,620,400,60),message);
			if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
		}
	}
}