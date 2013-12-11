package com.kumakore.sample.test;

import com.kumakore.KumakoreApp;

public class TestBase {
	private static final String TAG = TestBase.class.getName();

	private KumakoreApp _app;

	protected TestBase(KumakoreApp app) {
		_app = app;
	}
	
	public void run() {
		onRun();	
	}
	
	protected void onRun() {
		
	}	

	protected KumakoreApp app() {
		return _app;
	}
}
