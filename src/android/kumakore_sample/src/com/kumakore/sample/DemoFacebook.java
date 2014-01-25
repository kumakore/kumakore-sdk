package com.kumakore.sample;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import com.facebook.Session;
import com.kumakore.ActionFacebookGetFriends;
import com.kumakore.ActionFacebookLogin;
import com.kumakore.ActionFacebookConnect;
import com.kumakore.FriendFacebook;
import com.kumakore.FriendFacebookList;
import com.kumakore.ActionFacebookDeauthorize;
import com.kumakore.KumakoreApp;

public class DemoFacebook extends Activity implements ActionFacebookGetFriends.IKumakore, ActionFacebookLogin.IKumakore,
ActionFacebookConnect.IKumakore, ActionFacebookDeauthorize.IKumakore{
	private static final String TAG = DemoFacebook.class.getName();

	private ListView listViewFaceFriends;
	private Button startMatch;
	private int selectedPos;
	private KumakoreApp _app;

	private FriendFacebookList _facebookFriends;
	
	private ArrayList<FriendFacebook> _invitedFriends;
	private ArrayList<FriendFacebook> _friends;
	
	private FacebookHelper fbHelper;
	
	public DemoFacebook() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.facebook);

		// get instance of KumakoreApp.
		_app = new KumakoreApp(Helpers.GetApiKey(), Helpers.GetDashboardVersion(), Helpers.GetAppVersion(), this);

		getWidgets();
		drawButtons();
		updateView();
		
		fbHelper = new FacebookHelper(this);
		fbHelper.CheckSession();
		if(!fbHelper.isSessionOpened())
			fbHelper.requestLogin();
	}
	   
	private void getWidgets() {
		listViewFaceFriends = (ListView) findViewById(R.id.friendsListView);
		startMatch = (Button) findViewById(R.id.btnStartFaceMatch);

		listViewFaceFriends.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

				// selected item
				Log.v(TAG, "onItemSelected(..., " + position + ",...) => selected: " + listViewFaceFriends.getSelectedItem());
				selectedPos = position;

				listViewFaceFriends.smoothScrollToPosition(position);
			}

		});

	}

	private void drawButtons() {
		Button btGetFriends = (Button) findViewById(R.id.btnGetFriends);
		btGetFriends.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {				
				if(fbHelper.isSessionOpened())
					_app.getUser().getFacebookFriends().get(fbHelper.getToken()).async(DemoFacebook.this);
			}
		});

		Button btStartMatch = (Button) findViewById(R.id.btnStartFaceMatch);
		btStartMatch.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				startMatch();
			}
		});

		Button btLogout = (Button) findViewById(R.id.btnFBLogout);
		btLogout.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				if(fbHelper.isSessionOpened())
					fbHelper.requestLogout();
			}
		});
		
		Button btConnect = (Button) findViewById(R.id.btnFacebookConnect);
		btConnect.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				if(fbHelper.isSessionOpened())
				_app.getUser().facebookConnectAccount(fbHelper.getToken()).async(DemoFacebook.this);
			}
		});
		
		Button btDeauthorize = (Button) findViewById(R.id.btnRemoveFace);
		btDeauthorize.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {				
				_app.getUser().facebookDeauthorizeAccount().async(DemoFacebook.this);
			}
		});

	}

	private void startMatch() {

		TextView tv = (TextView) listViewFaceFriends.getChildAt(selectedPos);
		String playerName = tv.getText().toString();

		// TODO: (Otto) check if FB friend then start match

	}

	private void updateView() {
		if (Session.getActiveSession() != null)
			startMatch.setEnabled(true);
		else
			startMatch.setEnabled(false);
	}

	private void fillFriendList() {

		List<String> list = new ArrayList<String>();
		for (FriendFacebook friend : _friends) {
			list.add(friend.getFirstName() + " " + friend.getLastName());
		}

		for (FriendFacebook friend : _invitedFriends) {
			list.add(friend.getFirstName() + " " + friend.getLastName());
		}

		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, list);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		listViewFaceFriends.setAdapter(dataAdapter);
	}

	@Override
	public void onActionFacebookGetFriends(ActionFacebookGetFriends action) {
		_facebookFriends = _app.getUser().getFacebookFriends();
		_invitedFriends = _facebookFriends.getInvitedFriends();
		_friends = _facebookFriends.getFriends();
		fillFriendList();
		updateView();
	}

	@Override
	public void onActionFacebookLogin(ActionFacebookLogin action) {

		Log.i(TAG, "onActionFacebookLogin ");
		updateView();
	}

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		Log.v(TAG, "onActivityResult " + resultCode);

		Session.getActiveSession().onActivityResult(this, requestCode, resultCode, data);
	}

	@Override
	protected void onSaveInstanceState(Bundle outState) {
		super.onSaveInstanceState(outState);
		Log.v(TAG, "onSaveInstanceState ");
		Session session = Session.getActiveSession();
		Session.saveSession(session, outState);
	}

	@Override
	public void onActionFacebookConnect(ActionFacebookConnect action) {
		Log.v(TAG, "onActionFacebookConnectAccount ");
	}

	@Override
	public void onActionFacebookDeauthorize(ActionFacebookDeauthorize action) {
		Log.v(TAG, "onActionFacebookRemoveAccount ");
	}

}
