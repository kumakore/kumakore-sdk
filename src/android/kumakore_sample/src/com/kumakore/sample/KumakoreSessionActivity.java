package com.kumakore.sample;

import android.content.Intent;
import android.os.Bundle;

/**
 * KumakoreSessionActivity is a simple example of how to maintain a Kumakore user session
 */
public class KumakoreSessionActivity extends KumakoreActivity {

	@SuppressWarnings("unused")
	private static final String TAG = KumakoreSessionActivity.class.getName();

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	}
    
	@Override
	protected void onResume() {
		super.onResume();
		
		// verify User Session
		// not logged in
		if (!app().user().hasId()) {
			Intent intent = new Intent(KumakoreSessionActivity.this, TrySignupSigninActivity.class);
			startActivityForResult(intent, TrySignupSigninActivity.REQUEST_CODE);
		}
	}
}