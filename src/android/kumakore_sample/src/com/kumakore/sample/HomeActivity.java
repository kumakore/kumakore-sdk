package com.kumakore.sample;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;

/**
 * HomeActivity is a simple example of how to use the kumakore SDK
 */
public class HomeActivity extends KumakoreSessionActivity {

	private static final String TAG = HomeActivity.class.getName();

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_home);
		
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
	}

	private void checkSession() {
		// not logged in
		if (!app().user().hasSessionId()) {
			Intent intent = new Intent(HomeActivity.this, TrySignupSigninActivity.class);
			startActivity(intent);
			return;
		}
	}
}