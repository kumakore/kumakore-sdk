using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.kumakore;

namespace com.kumakore.sample {

	public partial class AchievementsBehav : SigninBehav {
		
		protected override void OnGUI () {
			if(app.getUser ().hasSessionId()) {
				// Buttons / actions
				if(GUI.Button (new Rect(10,10,200,60),"Load app achievements")) loadAppAchievements();
				if(GUI.Button (new Rect(10,80,200,60),"Load user achievements")) loadUserAchievements();
	//			if(	app.getAchievements().Count > 0 &&
	//				GUI.Button (new Rect(10,150,200,60),"Progress random achievement"))
	//					SetAchievement(app.getAchievements()[Random.Range (0,app.getAchievements ().Count)].getName (),10);
				
				// display
				GUI.Box (new Rect(395,5,305,365),"AppAchievements");
				
				foreach(AppAchievement ach in app.getAchievements ().Values) {
					//int min = ii * 120;
					int min = 120;
					GUI.Label (new Rect(400,30+min,300,40),"Name: " + ach.getName ());
					GUI.Label (new Rect(400,75+min,300,40),"Description: " + ach.getDescription ());
				}
				
				GUI.Box (new Rect(395,370,305,365),"User Achievements");
				
				foreach(UserAchievement ach in app.getUser().getAchievements ().Values) {
					//int min = 370+ii * 120;
					int min = 370 * 120;
					GUI.Label (new Rect(400,30+min,300,40),"Name: " + ach.getName ());
					GUI.Label (new Rect(400,75+min,300,40),"Description: " + ach.getProgress ());
				}
			} else {
				base.OnGUI();
			}
			
//			GUI.Label (new Rect(10,620,400,60),message);
			if(GUI.Button(new Rect(10,680,150,60),"Quit")) Application.Quit();
		}
	}
}