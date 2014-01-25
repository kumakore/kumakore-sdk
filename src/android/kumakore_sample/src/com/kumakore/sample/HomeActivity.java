package com.kumakore.sample;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;

import com.kumakore.ActionAppGetRewardMap;
import com.kumakore.ActionDeviceRegister;
import com.kumakore.ActionFriendGetInvitations;
import com.kumakore.ActionFriendGetInvited;
import com.kumakore.ActionFriendInvitationResponse;
import com.kumakore.ActionFriendInvite;
import com.kumakore.FriendList.InviteResponse;
import com.kumakore.StatusCodes;
import com.kumakore.sample.test.TestFriends;

/**
 * HomeActivity is a simple example of how to use the kumakore SDK
 */
public class HomeActivity extends KumakoreSessionActivity {

	private static final String TAG = HomeActivity.class.getName();

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_home);
		
		TestFriends testFriends = new TestFriends(app());
		testFriends.run();

		// User Settings
		Button btnUser = (Button) findViewById(R.id.btnUser);
		btnUser.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {

				Intent intent = new Intent(HomeActivity.this, UserSettingsActivity.class);
				startActivity(intent);
			}
		});

		// Achievements
		Button btnAchivement = (Button) findViewById(R.id.btnAchivement);
		btnAchivement.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Intent intent = new Intent(HomeActivity.this, AchievementsActivity.class);
				startActivity(intent);
			}
		});

		// Inventory and Store
		Button btnInventory = (Button) findViewById(R.id.btnInventory);
		btnInventory.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Intent intent = new Intent(HomeActivity.this, InventoryActivity.class);
				startActivity(intent);
			}
		});

		// Matches
		Button btnMatch = (Button) findViewById(R.id.btnMatch);
		btnMatch.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Intent intent = new Intent(HomeActivity.this, MatchListActivity.class);
				startActivity(intent);
			}
		});
	}

	@Override
	public void onResume() {
		super.onResume();
		checkSession();

		registerForPush();
	}

	private void checkSession() {
		// not logged in
		if (!app().getUser().hasSessionId()) {
			Intent intent = new Intent(HomeActivity.this, TrySignupSigninActivity.class);
			startActivity(intent);
			return;
		}
	}

	private void registerForPush() {
		app().getUser().device().register(Helpers.GetGsmSenderId())
				.async(new ActionDeviceRegister.IKumakore() {

					@Override
					public void onActionDeviceRegister(com.kumakore.ActionDeviceRegister action, String token) {
						if (action.getStatusCode() == StatusCodes.SUCCESS) {
							Log.i(TAG, "registered for push");
						} else {
							Log.w(TAG, "Failed to register for push");
						}
					}
				});
	}
}