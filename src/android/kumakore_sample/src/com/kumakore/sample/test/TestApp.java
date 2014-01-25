package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionAppGetRewardMap;
import com.kumakore.ActionAppPlatform;
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
	
	public void testAppPlatform()
	{
		app().getPlatform().async(new ActionAppPlatform.IKumakore() {

			@Override
			public void onActionAppPlatform(ActionAppPlatform action) {
				// TODO Auto-generated method stub
				Log.i(TAG, "response " + action.getCurrent());
				Log.i(TAG, "response " + action.getMinimum());
			}
		});
	}
	public void testGetRewards()
	{
		app().getRewards().get().async(new ActionAppGetRewardMap.IKumakore() {

			@Override
			public void onActionAppGetRewardMap(ActionAppGetRewardMap action) {
				Log.i(TAG, "qweqweqweqw ");
			}
		});
	}
}
