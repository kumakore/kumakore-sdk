package com.kumakore.sample;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map.Entry;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.kumakore.ActionMatchGetClosed;
import com.kumakore.ActionMatchClose;
import com.kumakore.ActionMatchCreate;
import com.kumakore.ActionMatchCreateRandom;
import com.kumakore.ActionMatchGetStatus;
import com.kumakore.ActionMatchGetOpen;
import com.kumakore.Match;
import com.kumakore.OpenMatch;
import com.kumakore.OpenMatchMap;
import com.kumakore.StatusCodes;
import com.kumakore.listactivity.MatchButton;

public class MatchListActivity extends KumakoreSessionActivity implements ActionMatchGetOpen.IKumakore, ActionMatchClose.IKumakore,
		ActionMatchCreate.IKumakore, ActionMatchCreateRandom.IKumakore, ActionMatchGetClosed.IKumakore, ActionMatchGetStatus.IKumakore {
	private static final String TAG = MatchListActivity.class.getName();

	private LinearLayout _layoutMyTurn;
	private LinearLayout _layoutTheirTurn;
	private LinearLayout _layoutClosedMatches;

	private EditText _editTextOpponentName;

	private OpenMatchMap _openMatchMap;

	private HashMap<String, OpenMatch> myTurnMatches;
	private HashMap<String, OpenMatch> theirTurnMatches;
	private HashMap<String, Match> completedMatches;

	private boolean _finishedCurrentMatches, _finishedCompletedMatches;

	private String _selectedMatchId;

	private ProgressDialog _dialog;

	public MatchListActivity() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_matchlist);

		getWidgets();

		drawMatchCreation();

		downloadMatches();

		this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
	}

	private void getWidgets() {
		_layoutMyTurn = (LinearLayout) findViewById(R.id.layout_myTurn);
		_layoutTheirTurn = (LinearLayout) findViewById(R.id.layout_theirTurn);
		_layoutClosedMatches = (LinearLayout) findViewById(R.id.layout_completed_matches);

		_editTextOpponentName = (EditText) findViewById(R.id.opponent_name);
	}

	private void downloadMatches() {
		_finishedCompletedMatches = _finishedCurrentMatches = false;
		_openMatchMap = app().getUser().getOpenedMatches();
		_openMatchMap.get().async(MatchListActivity.this);
		app().getUser().getClosedMatches().get().async(MatchListActivity.this);

		_dialog = ProgressDialog.show(MatchListActivity.this, "", "Loading. Please wait...", true);
	}

	private void drawMatchCreation() {
		Button buttonCreateNewMatch = (Button) findViewById(R.id.button_create_match);
		buttonCreateNewMatch.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				hideKeyboard();
				createMatch();
			}
		});

		Button buttonRandomMatch = (Button) findViewById(R.id.button_random_match);
		buttonRandomMatch.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				_openMatchMap.createRandomMatch().async(MatchListActivity.this);
				hideKeyboard();
			}
		});
	}

	private void updateAllMatches() {

		if (!_finishedCompletedMatches || !_finishedCurrentMatches)
			return;

		updateCurrentMatchList();
		updateCompletedMatchList();

		_dialog.dismiss();
	}

	private void updateCurrentMatchList() {

		// Store variables
		myTurnMatches = _openMatchMap.getMyTurn();
		theirTurnMatches = _openMatchMap.getTheirTurn();
		completedMatches = app().getUser().getClosedMatches();

		_layoutMyTurn.removeAllViews();
		if (myTurnMatches.size() > 0)
			drawMyTurnMatches();

		_layoutTheirTurn.removeAllViews();
		if (theirTurnMatches.size() > 0)
			drawTheirTurnMatches();

	}

	private void drawMyTurnMatches() {

		LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);

		// Title
		TextView tv = new TextView(this);
		tv.setText("My Turn");
		tv.setTextColor(Color.rgb(100, 100, 255));
		tv.setTextSize(20);
		tv.setLayoutParams(lp);
		_layoutMyTurn.addView(tv);

		List<String> matches = new ArrayList<String>();
		MatchButton matchButton;
		OpenMatch openMatch;
		for (Entry<String, OpenMatch> entry : myTurnMatches.entrySet()) {
			openMatch = entry.getValue();
			matches.add(openMatch.getOpponentUsername());
			matchButton = new MatchButton(this, openMatch, true);
			_layoutMyTurn.addView(matchButton);
		}
	}

	private void drawTheirTurnMatches() {

		LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);

		TextView tv = new TextView(this);
		tv.setText("Their Turn");
		tv.setTextColor(Color.GRAY);
		tv.setTextSize(20);
		lp.topMargin = 40;
		tv.setLayoutParams(lp);
		_layoutTheirTurn.addView(tv);

		List<String> matches = new ArrayList<String>();
		MatchButton matchButton;
		OpenMatch openMatch;
		for (Entry<String, OpenMatch> entry : theirTurnMatches.entrySet()) {
			openMatch = entry.getValue();
			matches.add(openMatch.getOpponentUsername());
			matchButton = new MatchButton(this, openMatch, false);
			_layoutTheirTurn.addView(matchButton);
		}
		/*
		 * // handle click on match listViewTheirTurn.setOnItemClickListener(new
		 * AdapterView.OnItemClickListener() { public void
		 * onItemClick(AdapterView<?> parentAdapter, View view, int position,
		 * long id) { String matchId =
		 * theirTurnMatches.get(position).getMatchId();
		 * 
		 * Log.i(TAG, matchId);
		 * theirTurnMatches.get(matchId).getStatus().async(MatchListActivity
		 * .this); } });
		 */
	}

	private void updateCompletedMatchList() {

		_layoutClosedMatches.removeAllViews();

		LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);

		TextView tv = new TextView(this);
		tv.setText("Completed Matches");
		tv.setTextSize(20);
		tv.setTextColor(Color.RED);
		lp.topMargin = 60;
		tv.setLayoutParams(lp);
		_layoutClosedMatches.addView(tv);

		List<String> matches = new ArrayList<String>();
		for (Entry<String, Match> entry : completedMatches.entrySet()) {
			matches.add(entry.getValue().getOpponentUsername());
		}
	}

	private void createMatch() {
		String opponentName = _editTextOpponentName.getText().toString();
		Log.i(TAG, "name:" + opponentName);
		// Check if empty and return
		if (opponentName.isEmpty()) {
			Toast toast = Toast.makeText(this, "Enter the opponent name", Toast.LENGTH_SHORT);
			toast.show();
			return;
		}
		_openMatchMap.createNewMatch(opponentName).async(MatchListActivity.this);
	}

	@Override
	public void onActionMatchClose(ActionMatchClose action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			_finishedCompletedMatches = true;
			updateAllMatches();

		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.setGravity(Gravity.TOP, 0, 10);
			toast.show();
		}
	}

	@Override
	public void onActionMatchCurrentListGet(ActionMatchGetOpen action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			_finishedCurrentMatches = true;
			updateAllMatches();
			// Log.i(TAG, "current matchlist count: " + currentMatches.size());
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.setGravity(Gravity.TOP, 0, 10);
			toast.show();
		}
	}

	@Override
	public void onActionMatchCreate(ActionMatchCreate action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			Log.i(TAG, "Match Created SUCCESS ");
			downloadMatches();
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.setGravity(Gravity.TOP, 0, 10);
			toast.show();
		}
	}

	@Override
	public void onActionMatchCreateRandom(ActionMatchCreateRandom action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			Log.i(TAG, "Random Match Created SUCCESS ");
			downloadMatches();
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.setGravity(Gravity.TOP, 0, 10);
			toast.show();
		}
	}

	@Override
	public void onActionMatchGetStatus(ActionMatchGetStatus action) {

		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			// Log.i(TAG, "current match: " +
			// _openMatchMap.get(_selectedMatchId).getMatchId());

			_finishedCompletedMatches = _finishedCurrentMatches = false;

			// start match info activity
			Intent intent = new Intent(MatchListActivity.this, MatchInfoActivity.class);
			intent.putExtra("matchId", _selectedMatchId);
			startActivity(intent);

		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.setGravity(Gravity.TOP, 0, 10);
			toast.show();
		}
	}

	@Override
	protected void onRestart() {
		super.onRestart();

		// Refresh match lists
		downloadMatches();
	}

	private void hideKeyboard() {
		InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(_editTextOpponentName.getWindowToken(), 0);
	}

	public void RequestMatchStatus(Match match) {
		_selectedMatchId = match.getMatchId();
		match.getStatus().async(MatchListActivity.this);
	}

	@Override
	public void onActionMatchCompletedListGet(ActionMatchGetClosed action) {
		// TODO Auto-generated method stub
		
	}
}
