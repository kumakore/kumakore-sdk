using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class AchievementsScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI () {
		if(kumakore.user ().hasSessionId()) {
			// Buttons / actions
			if(GUI.Button (new Rect(10,10,200,60),"Load app achievements")) kumakore.achievements ().get ().sync (delegate(ActionAppAchievementListGet action) {
				Debug.Log ("App achievements loaded");
			});
			if(GUI.Button (new Rect(10,80,200,60),"Load user achievements")) kumakore.user().achievements ().get ().sync (delegate(ActionUserAchievementListGet action) {
				Debug.Log ("User achievements loaded");
			});
			if(kumakore.achievements().Count > 0) if(GUI.Button (new Rect(10,150,200,60),"Progress random achievement")) kumakore.user().achievements ().set(kumakore.achievements()[Random.Range (0,kumakore.achievements ().Count)].getName (),10).sync (delegate(ActionUserAchievementSet action) {
				Debug.Log ("Achievement set");
			});
			
			// display
			GUI.Box (new Rect(395,5,305,365),"AppAchievements");
			for(int ii=0; ii< kumakore.achievements().Count; ii++) {
				int min = ii * 120;
				AppAchievement ach = kumakore.achievements ()[ii];
				GUI.Label (new Rect(400,30+min,300,40),"Name: " + ach.getName ());
				GUI.Label (new Rect(400,75+min,300,40),"Description: " + ach.getDescription ());
			}
			GUI.Box (new Rect(395,370,305,365),"User Achievements");
			for(int ii=0; ii< kumakore.user().achievements().Count; ii++) {
				int min = 370+ii * 120;
				UserAchievement ach = kumakore.user().achievements ()[ii];
				GUI.Label (new Rect(400,30+min,300,40),"Name: " + ach.getName ());
				GUI.Label (new Rect(400,75+min,300,40),"Description: " + ach.getProgress ());
			}
		} else {
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) kumakore.signin (useremail,password).async (SigninDelegate);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	
	// DELEGATES
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
	}
	
}