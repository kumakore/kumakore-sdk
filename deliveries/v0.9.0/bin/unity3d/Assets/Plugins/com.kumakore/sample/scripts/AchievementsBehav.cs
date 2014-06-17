using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.kumakore;

namespace com.kumakore.sample {

	public partial class AchievementsBehav : SigninBehav {
		
		private static readonly string TAG = typeof(AchievementsBehav).Name;	

		public void loadAppAchievements() {
			app.getAchievements ().get ().sync (delegate(ActionAchievementGetApp action) {
				Kumakore.LOGI (TAG, "App achievements loaded");
			});
		}

		public void loadUserAchievements() {
			app.getUser().getAchievements ().get ().sync (delegate(ActionAchievementGetUser action) {
				Kumakore.LOGI (TAG, "User achievements loaded");
			});
		}

		public void setAchievement(string achievement, int progress) {
			app.getUser().getAchievements ().set(achievement,progress).sync (delegate(ActionAchievementSetUser action) {
				Kumakore.LOGI (TAG, "Achievement set");
			});
		}
	}
}