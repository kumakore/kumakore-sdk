package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionFacebookGetFriends;
import com.kumakore.ActionFacebookLogin;
import com.kumakore.KumakoreApp;
import com.kumakore.sample.Helpers;

public class TestFacebook extends TestBase implements ActionFacebookLogin.IKumakore, ActionFacebookGetFriends.IKumakore {
	private static final String TAG = TestFacebook.class.getName();

	public TestFacebook(KumakoreApp app) {
		super(app);
	}

	@Override
	protected void onRun() {
		testFBLogin();
		//testGetFriends();
	}

	public void testFBLogin() {
		app().facebookLogin(Helpers.GetFacebookToken()).async(TestFacebook.this);		
	}
	
	public void testGetFriends(){
		app().user().getFacebookFriends().get(Helpers.GetFacebookToken()).async(TestFacebook.this);
	}

	@Override
	public void onActionFacebookLogin(ActionFacebookLogin action) {
		Log.i(TAG, "LOGIN SUCCESS ");
		testGetFriends();
	}

	@Override
	public void onActionFacebookGetFriends(ActionFacebookGetFriends action) {
		Log.i(TAG, "GET FRIENDS SUCCESS");
		Log.i(TAG, "INVITE FRIENDS: " + app().user().getFacebookFriends().inviteFriends.size());
		Log.i(TAG, "GAME FRIENDS: " + app().user().getFacebookFriends().friends.size());
		
	}

}
