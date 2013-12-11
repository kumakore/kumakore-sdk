package com.kumakore.sample;

import java.util.Arrays;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;

import com.facebook.*;

public class FacebookHelper {

	private static final String TAG = FacebookHelper.class.getName();
	private Session.StatusCallback statusCallback = new SessionStatusCallback();

	private Context _context;
	private Bundle _savedInstanceState;
	private Activity _activity;	
	private String _lastError;

	/* package */FacebookHelper(Activity activity) {
		_activity = activity;
		_context = (Context) activity;
		_savedInstanceState = activity.getIntent().getExtras();
	}

	public boolean HasLastError() {	
		return _lastError != null;
	}
	
	public String GetLastError() {	
		return _lastError;
	}
	
	public void CheckSession() {
		/*
		 * _context = context; _savedInstanceState = savedInstanceState;
		 * _activity = activity;
		 */

		Settings.addLoggingBehavior(LoggingBehavior.INCLUDE_ACCESS_TOKENS);

		Session session = Session.getActiveSession();

		if (session == null) {
			// Log.i(TAG, "null session");
			if (_savedInstanceState != null) {
				// Log.i(TAG, "Case 1");
				session = Session.restoreSession(_context, null, statusCallback, _savedInstanceState);
			}
			if (session == null) {
				// Log.i(TAG, "Case 2");
				session = new Session(_context);
			}
			// Log.i(TAG, "Case 3");
			Session.setActiveSession(session);
			if (session.getState().equals(SessionState.CREATED_TOKEN_LOADED)) {
				// Log.i(TAG, "Case 4");
				session.openForRead(new Session.OpenRequest(_activity).setCallback(statusCallback));
			}
			updateView();
		}

		session.addCallback(statusCallback);
	}

	private void updateView() {
		Session session = Session.getActiveSession();
		if (session.isOpened()) {
			// Log.i(TAG, "TOKEN" + session.getAccessToken());

		} else {
			// Log.i(TAG, "NOT LOGGED" + session.getAccessToken());
		}
	}

	public void requestLogin() {
		Session session = Session.getActiveSession();

		if (!session.isOpened() && !session.isClosed()) {
			// Log.i(TAG, "LOGIN 1");
			session.openForRead(new Session.OpenRequest(_activity)
					.setPermissions(Arrays.asList("basic_info")).setCallback(statusCallback));
		} else {
			// Log.i(TAG, "LOGIN 2");
			Session.openActiveSession(_activity, true, statusCallback);
		}
	}

	public void requestLogout() {
		Session session = Session.getActiveSession();
		if (!session.isClosed()) {
			session.closeAndClearTokenInformation();
		}
	}

	public String getToken() {
		Session session = Session.getActiveSession();
		if (session != null)
			return session.getAccessToken();
		else
			return "";
	}

	public boolean isSessionOpened() {
		Session session = Session.getActiveSession();
		if (session != null)
			return session.isOpened();
		else
			return false;
	}

	private class SessionStatusCallback implements Session.StatusCallback {
		@Override
		public void call(Session session, SessionState state, Exception exception) {
			if (exception != null) 
				_lastError = exception.getMessage();
			else
				_lastError = null;
			
			Log.i(TAG, "CALLBACK" + state.isOpened());
			updateView();
		}
	}
	
	
}
