package com.kumakore.sample.test;

import java.util.ArrayList;

import android.util.Log;

import com.kumakore.ActionFriendGetInvitations;
import com.kumakore.ActionFriendGetInvited;
import com.kumakore.ActionFriendInvitationResponse;
import com.kumakore.ActionFriendInvite;
import com.kumakore.FriendInvitation;
import com.kumakore.FriendList.InviteResponse;
import com.kumakore.KumakoreApp;

public class TestFriends extends TestBase {
	private static final String TAG = TestInventory.class.getName();

	private ArrayList<FriendInvitation> _friendInvitations;

	public TestFriends(KumakoreApp app) {
		super(app);
	}

	@Override
	protected void onRun() {
		testGetInvitedFriends();
		//testGetFriendInvitations();
		//testSendFriendInvite("bozo");
	}

	public void testGetInvitedFriends() {
		app().getUser().getFriends().getInvitedFriends().async(new ActionFriendGetInvited.IKumakore() {

			@Override
			public void onActionFriendGetInvited(ActionFriendGetInvited action) {

			}
		});
	}

	public void testGetFriendInvitations() {
		app().getUser().getFriends().getFriendInvitations().async(new ActionFriendGetInvitations.IKumakore() {

			@Override
			public void onActionFriendGetInvitations(ActionFriendGetInvitations action) {
				_friendInvitations = app().getUser().getFriends().friendInvitations;

				if (_friendInvitations.size() > 0) {
					Log.i(TAG, _friendInvitations.get(0).getMessage());
					testSendInviteResponse();
				}
			}
		});
	}

	public void testSendInviteResponse() {
		String id = app().getUser().getFriends().friendInvitations.get(0).getId();
		app().getUser().getFriends().sendInviteResponse(id, InviteResponse.accept, "for sure my friend")
				.async(new ActionFriendInvitationResponse.IKumakore() {

					@Override
					public void onActionFriendInvitationResponse(ActionFriendInvitationResponse action) {
						Log.i(TAG, app().getUser().getFriends().friendInvitations.get(0).getMessage());

					}
				});
	}

	public void testSendFriendInvite(String name) {
		app().getUser().getFriends().sendFriendInvite(name, "hey add me")
				.async(new ActionFriendInvite.IKumakore() {

					@Override
					public void onActionFriendInvite(ActionFriendInvite action) {
						Log.i(TAG, "invitation sent ");
					}
				});
	}
}
