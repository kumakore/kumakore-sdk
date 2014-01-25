package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionAchievementGetApp;
import com.kumakore.ActionUserAchievementListGet;
import com.kumakore.ActionUserAchievementSet;
import com.kumakore.AppAchievement;
import com.kumakore.KumakoreApp;
import com.kumakore.UserAchievement;

public class TestAchievements extends TestBase {
	private static final String TAG = TestAchievements.class.getName();

	public TestAchievements(KumakoreApp app) {
		super(app);
	}

	@Override
	protected void onRun() {
		testAppAchievements();
		testUserAchievements();
	}

	public void testAppAchievements() {

		// ## example 9 : get app achievements ##
		app().getAchievements().get().async(new ActionAchievementGetApp.IKumakore() {
			@Override
			public void onActionAppAchievmentListGet(ActionAchievementGetApp action) {

				Log.i(TAG, "Achievements size " + app().getAchievements().size());
				for (AppAchievement achievement : app().getAchievements().values()) {
					Log.i(TAG, achievement.getName() + "  " + achievement.getDescription());
				}
			}

		});
	}

	public void testUserAchievements() {

		// ## example 7 : get user achievements ##
		app().getUser().achievements().get().async(new ActionUserAchievementListGet.IKumakore() {

			@Override
			public void onActionUserAchievmentListGet(ActionUserAchievementListGet action) {
				Log.i(TAG, "User Achievements size " + app().getUser().achievements().size());
				for (UserAchievement achievement : app().getUser().achievements().values()) {
					Log.i(TAG, achievement.getName() + "  " + achievement.getProgress());
				}
				testChangeProgress();
			}
		});

		
	}
	
	private void testChangeProgress() {
		UserAchievement achievement = app().getUser().achievements().get(0);
		Log.i(TAG, "Progress is " + achievement.getProgress());
		
		app().getUser().achievements().set(achievement.getName(), achievement.getProgress() + 1)
				.async(new ActionUserAchievementSet.IKumakore() {

					@Override
					public void onActionUserAchievmentSet(ActionUserAchievementSet action) {
						for (UserAchievement achievement : app().getUser().achievements().values()) {

							Log.i(TAG, achievement.getName() + "  " + achievement.getProgress());
						}
					}
				});
	}  
}
