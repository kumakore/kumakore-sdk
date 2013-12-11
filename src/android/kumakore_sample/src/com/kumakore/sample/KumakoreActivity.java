package com.kumakore.sample;

import android.app.Activity;
import android.os.Bundle;
import com.kumakore.KumakoreApp;

/**
 * KumakoreActivity is a simple example of how to create a KumakoreApp
 */
public class KumakoreActivity extends Activity {

	@SuppressWarnings("unused")
	private static final String TAG = KumakoreActivity.class.getName();

	private KumakoreApp _app;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		// get instance of KumakoreApp.
		_app = new KumakoreApp(Helpers.GetApiKey(), Helpers.GetDashboardVersion(), Helpers.GetAppVersion(),
						this);
	}

	protected KumakoreApp app() {
		return _app;
	}	
	
	@Override
	protected void onResume() {
		super.onResume();
		
		// reload preferences that could have been changed in a different instance
		_app.refresh();
	}
}