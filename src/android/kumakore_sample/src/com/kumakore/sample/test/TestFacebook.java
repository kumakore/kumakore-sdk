package com.kumakore.sample.test;

import android.util.Log;

import com.kumakore.ActionFacebookGetFriends;
import com.kumakore.ActionFacebookSignin;
import com.kumakore.KumakoreApp;
import com.kumakore.sample.Helpers;

public class TestFacebook extends TestBase implements ActionFacebookSignin.IKumakore, ActionFacebookGetFriends.IKumakore {
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
		app().getUser().getFacebookFriends().get(Helpers.GetFacebookToken()).async(TestFacebook.this);
	}

	@Override
	public void onActionFacebookSignin(ActionFacebookSignin action) {
		Log.i(TAG, "LOGIN SUCCESS ");
		testGetFriends();
	}

	@Override
	public void onActionFacebookGetFriends(ActionFacebookGetFriends action) {
		Log.i(TAG, "GET FRIENDS SUCCESS");
		Log.i(TAG, "INVITE FRIENDS: " + app().getUser().getFacebookFriends().getInvitedFriends().size());
		Log.i(TAG, "GAME FRIENDS: " + app().getUser().getFacebookFriends().getFriends().size());
		
	}

}
