package com.kumakore.sample.test;

import com.kumakore.KumakoreApp;

public class TestApp extends TestBase {
	private static final String TAG = TestApp.class.getName();
	
	public TestApp(KumakoreApp app) {
		super(app);
	}
	
	@Override
	protected void onRun() {
		testLog();			
	}
	
	public void testLog() {
		// log
		// Log.e(TAG, Thread.currentThread().getName() +
		// " is waiting on barrier");
		app().logInfo("log info test").sync();
		// Log.e(TAG, Thread.currentThread().getName() +
		// " has crossed the barrier");
		app().logError("log error test").async();
	}
}
