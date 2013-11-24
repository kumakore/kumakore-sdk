package com.kumakore.sample.test;

import android.util.Log;

import com.google.android.gms.internal.ap;
import com.kumakore.ActionOpenMatchMap;
import com.kumakore.ActionMatchCreateNew;
import com.kumakore.ActionClosedMatchMap;
import com.kumakore.ActionMatchCreateRandom;
import com.kumakore.KumakoreApp;

public class TestMatches extends TestBase implements ActionOpenMatchMap.IKumakore, ActionMatchCreateNew.IKumakore,
ActionClosedMatchMap.IKumakore, ActionMatchCreateRandom.IKumakore {
	private static final String TAG = TestMatches.class.getName();

	public TestMatches(KumakoreApp app) {
		super(app);
	}

	@Override
	protected void onRun() {
		testGetCurrentMatches();
		testGetCompletedMatches();
		// testCreateNewMatch();
		testCreateRandomMatch();
	}

	public void testGetCurrentMatches() {
		app().user().getOpenedMatches().get().async(TestMatches.this);
	}

	public void testGetCompletedMatches() {
		app().user().getClosedMatches().get().async(TestMatches.this);
	}

	public void testCreateNewMatch() {
		// app().currentMatch().createNewMatch("qqqq").async(TestMatches.this);
	}

	public void testCreateRandomMatch() {
		app().user().getOpenedMatches().createRandomMatch().async(TestMatches.this);
	}

	@Override
	public void onActionMatchCurrentListGet(ActionOpenMatchMap action) {
		Log.i(TAG, "CURRENT MATCH LIST SUCCESS");
	}

	@Override
	public void onActionMatchCompletedListGet(ActionClosedMatchMap action) {
		Log.i(TAG, "COMPLETED MATCH LIST SUCCESS");

	}

	@Override
	public void onActionMatchCreateNew(ActionMatchCreateNew action) {
		Log.i(TAG, "NEW MATCH CREATED");
	}

	@Override
	public void onActionMatchCreateRandom(ActionMatchCreateRandom action) {
		Log.i(TAG, "RANDOM MATCH CREATED");
	}

}
