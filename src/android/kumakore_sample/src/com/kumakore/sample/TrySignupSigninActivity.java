package com.kumakore.sample;

import com.facebook.Session;
import com.kumakore.ActionUserSignin;
import com.kumakore.ActionUserSignup;
import com.kumakore.ActionFacebookSignin;
import com.kumakore.StatusCodes;
import com.kumakore.User;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

/**
 * TrySignupSigninActivity is a simple example of how to use Signup and Signup
 */
public class TrySignupSigninActivity extends KumakoreActivity {

	private static final String TAG = TrySignupSigninActivity.class.getName();

	public static final int REQUEST_CODE = 1001;
    private static final int DIALOG_SIGNIN = 1002;
	private final Context _context = this;
	
	EditText _editUsernameOrEmail;

	private FacebookHelper _fbHelper;
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_try_signup_signin);

		_fbHelper = new FacebookHelper(this);
		
		// Username or Email
		_editUsernameOrEmail = (EditText) findViewById(R.id.editUsernameOrEmail);
		
		// GO!
		Button btnGo = (Button) findViewById(R.id.btnGo);
		btnGo.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {

				String usernameOrEmail = _editUsernameOrEmail.getText().toString();
				if (usernameOrEmail.isEmpty()) {
					Toast toast = Toast.makeText(_context, "Enter the username or email", Toast.LENGTH_SHORT);
					toast.show();
				} else {					
					trySignup(usernameOrEmail);
				}
			}
		});
		
		// Facebook Signin
		Button btnFacebookSignin = (Button) findViewById(R.id.btnFacebookSignin);
		btnFacebookSignin.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				
				_fbHelper.CheckSession();
				if (!_fbHelper.isSessionOpened()) {
					_fbHelper.requestLogin();
				} else {
					tryFacebookSignin();
				}
			}
		});
	}
	
    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
        case DIALOG_SIGNIN:
	        // This example shows how to add a custom layout to an AlertDialog
	        LayoutInflater factory = LayoutInflater.from(this);
	        final View textEntryView = factory.inflate(R.layout.alert_dialog_text_entry, null);

			
	        final EditText editUsername = (EditText) textEntryView.findViewById(R.id.username_edit);
	        final EditText editPassword = (EditText) textEntryView.findViewById(R.id.password_edit);
	        
	        editUsername.setText(_editUsernameOrEmail.getText().toString());
	        editPassword.requestFocus();
			editPassword.getText().clear();
			
	        return new AlertDialog.Builder(TrySignupSigninActivity.this)
	            //.setIcon(R.drawable.alert_dialog_icon)
	            .setTitle(R.string.alert_dialog_text_entry)
	            .setView(textEntryView)
	            .setPositiveButton(R.string.alert_dialog_ok, new DialogInterface.OnClickListener() {
	                public void onClick(DialogInterface dialog, int whichButton) {

	        			String usernameOrEmail = editUsername.getText().toString();
	        			String password = editPassword.getText().toString();

	    				if (usernameOrEmail.isEmpty()) {
	    					Toast toast = Toast.makeText(_context, "Invalid username or email", Toast.LENGTH_SHORT);
	    					toast.show();
	    				} else if (password.isEmpty()) {
	    					Toast toast = Toast.makeText(_context, "Invalid password", Toast.LENGTH_SHORT);
	    					toast.show();	    					
	    				} else {
		        			trySignin(usernameOrEmail, password);
		                }	    	
	                }
	            })
	            .setNegativeButton(R.string.alert_dialog_cancel, new DialogInterface.OnClickListener() {
	                public void onClick(DialogInterface dialog, int whichButton) {
	                	
	                	dialog.cancel();
	                }
	            })
	            .create();
        }
        return null;
    }
	
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		// facebook login result
		if (Session.getActiveSession().onActivityResult(this, requestCode, resultCode, data)) {
			tryFacebookSignin();
		}
	}
	
	private void trySignup(String usernameOrEmail) {
		
		app().signup(usernameOrEmail).async(new ActionUserSignup.IKumakore() {
			
			@Override
			public void onActionUserSignup(ActionUserSignup action) {
				if (action.getStatusCode() == StatusCodes.SUCCESS) {
					setResult(RESULT_OK);
					finish();
				} else {
					showDialog(DIALOG_SIGNIN);
				}
			}
		});		
	}
	
	private void trySignin(String usernameOrEmail, String password) {
		
		app().signin(usernameOrEmail, password).async(new ActionUserSignin.IKumakore() {
			
			@Override
			public void onActionUserSignin(ActionUserSignin action) {
				if (action.getStatusCode() == StatusCodes.SUCCESS) {
					setResult(RESULT_OK);
					finish();
				} else {
					showDialog(DIALOG_SIGNIN);
				}
			}
		});	
	}
	
	private void tryFacebookSignin() {	
	
		_fbHelper.CheckSession();
		if (_fbHelper.isSessionOpened()) {
			app().facebookLogin(_fbHelper.getToken()).async( new ActionFacebookSignin.IKumakore() {
				
				@Override
				public void onActionFacebookSignin(ActionFacebookSignin action) {
					if (action.getStatusCode() == StatusCodes.SUCCESS) {
						setResult(RESULT_OK);
						finish();
					}					
				}
			});
		} else if (_fbHelper.HasLastError()) {
			Toast toast = Toast.makeText(_context, _fbHelper.GetLastError(), Toast.LENGTH_SHORT);
			toast.show();
		}
	}
}