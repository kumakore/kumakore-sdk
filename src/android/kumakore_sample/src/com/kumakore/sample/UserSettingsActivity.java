package com.kumakore.sample;

import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.kumakore.ActionUserGet;
import com.kumakore.ActionUserUpdate;
import com.kumakore.StatusCodes;
import com.kumakore.User;

public class UserSettingsActivity extends KumakoreSessionActivity implements ActionUserUpdate.IKumakore, ActionUserGet.IKumakore {
	private static final String TAG = UserSettingsActivity.class.getName();

	private EditText _email;
	private EditText _username;	
	private EditText _password;

	private User _user;

	public UserSettingsActivity() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_user_settings);
		
		_user = app().user();		

		// Username 
		_username = (EditText) findViewById(R.id.editUsername);
		// Email
		_email = (EditText) findViewById(R.id.editEmail);
		// Password
		_password = (EditText) findViewById(R.id.editPassword);
		
		// Save
		Button btnSave = (Button) findViewById(R.id.btnSave);
		btnSave.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				save();
			}
		});

		// Signout
		Button btnSignout = (Button) findViewById(R.id.btnSignout);
		btnSignout.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				signout();
			}
		});
	}	
	
	@Override
	protected void onResume() {
		super.onResume();
		
		load();
	}

	private void load() {
		loadCache();
		_user.get().async(UserSettingsActivity.this);
	}

	private void loadCache() {
		_username.setText(_user.getName());
		_email.setText(_user.getEmail());
		_password.getText().clear();
	}
	
	private void save() {
		
		String username = _username.getText().toString();
		String email = _email.getText().toString();
		String password = _password.getText().toString();
		
		_user.update(username, email, password).async(UserSettingsActivity.this);
	}
	
	private void signout() {
		_user.signout().sync();
		finish();
	}

	@Override
	public void onActionUserUpdate(ActionUserUpdate action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			loadCache();
		} 
		
		Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
		toast.show();
	}

	@Override
	public void onActionUserGet(ActionUserGet action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			loadCache();
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.show();	
		}
	}
}
