using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class AchievementsScene : MonoBehaviour {
	#region demo variables
	public string appKey;
	public int dashboardVersion;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	#endregion
	
	#region Kumakore calls
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
		kumakore.load ();
	}
	
	
	private void Update() {
		kumakore.getDispatcher().dispatch();	
	}
	
	void OnDestroy() {
		kumakore.save ();
	}
	
	// Kumakore calls
	public void SignIn(string user, string pass) {
		kumakore.signin (user,pass).async (delegate(ActionUserSignin action) {
			message = "Signin delegate: " + action.getStatusMessage();
		});
	}
	public void LoadAppAchievements() {
		kumakore.getAchievements ().get ().sync (delegate(ActionAchievementGetApp action) {
			Debug.Log ("App achievements loaded");
		});
	}
	public void LoadUserAchievements() {
		kumakore.getUser().getAchievements ().get ().sync (delegate(ActionAchievementGetUser action) {
			Debug.Log ("User achievements loaded");
		});
	}
	public void SetAchievement(string achievement, int progress) {
		kumakore.getUser().getAchievements ().set(achievement,progress).sync (delegate(ActionAchievementSetUser action) {
			Debug.Log ("Achievement set");
		});
	}
	#endregion
	
	#region GUI
	
	void OnGUI () {
		if(kumakore.getUser ().hasSessionId()) {
			// Buttons / actions
			if(GUI.Button (new Rect(10,10,200,60),"Load app achievements")) LoadAppAchievements();
			if(GUI.Button (new Rect(10,80,200,60),"Load user achievements")) LoadUserAchievements();
//			if(	kumakore.getAchievements().Count > 0 &&
//				GUI.Button (new Rect(10,150,200,60),"Progress random achievement"))
//					SetAchievement(kumakore.getAchievements()[Random.Range (0,kumakore.getAchievements ().Count)].getName (),10);
			
			// display
			GUI.Box (new Rect(395,5,305,365),"AppAchievements");
			
			foreach(AppAchievement ach in kumakore.getAchievements ().Values) {
				//int min = ii * 120;
				int min = 120;
				GUI.Label (new Rect(400,30+min,300,40),"Name: " + ach.getName ());
				GUI.Label (new Rect(400,75+min,300,40),"Description: " + ach.getDescription ());
			}
			
			GUI.Box (new Rect(395,370,305,365),"User Achievements");
			
			foreach(UserAchievement ach in kumakore.getUser().getAchievements ().Values) {
				//int min = 370+ii * 120;
				int min = 370 * 120;
				GUI.Label (new Rect(400,30+min,300,40),"Name: " + ach.getName ());
				GUI.Label (new Rect(400,75+min,300,40),"Description: " + ach.getProgress ());
			}
		} else {
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) SignIn (useremail,password);
		}
		
		GUI.Label (new Rect(10,620,400,60),message);
		if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
	}
	
	#endregion
}