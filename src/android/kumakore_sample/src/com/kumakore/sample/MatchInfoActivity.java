package com.kumakore.sample;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import com.kumakore.ActionChatMessageGet.IKumakore;
import com.kumakore.ActionClosedMatchMap;
import com.kumakore.ActionMatchAccept;
import com.kumakore.ActionMatchClose;
import com.kumakore.ActionMatchReject;
import com.kumakore.ActionMatchResign;
import com.kumakore.ActionMatchStatusGet;
import com.kumakore.ActionMatchSelectItems;
import com.kumakore.ActionMatchMoves;
import com.kumakore.ActionMatchSendMove;
import com.kumakore.ActionChatMessageGet;
import com.kumakore.ActionChatMessageSend;
import com.kumakore.ActionOpenMatchMap;
import com.kumakore.ChatMessage;
import com.kumakore.KumakoreApp;
import com.kumakore.Match;
import com.kumakore.OpenMatchMap;
import com.kumakore.ActionMatchSendNudge;
import com.kumakore.OpenMatch;

public class MatchInfoActivity extends KumakoreSessionActivity implements ActionOpenMatchMap.IKumakore,
		ActionMatchStatusGet.IKumakore, ActionMatchClose.IKumakore, ActionMatchResign.IKumakore,
		ActionMatchReject.IKumakore, ActionMatchAccept.IKumakore, ActionMatchSelectItems.IKumakore,
		ActionMatchMoves.IKumakore, ActionMatchSendMove.IKumakore, ActionChatMessageGet.IKumakore,
		ActionChatMessageSend.IKumakore, ActionMatchSendNudge.IKumakore {
	private static final String TAG = MatchInfoActivity.class.getName();

	private LinearLayout layoutMyTurn, layoutTheirTurn, layoutChat;
	private TextView player1Name, player2Name, score;
	private EditText editTextChatMessage;

	private Match _match;
	private OpenMatch _openMatch;
	private String _matchId;
	private String _userId;

	private boolean _mystart = false, _isMyTurn;

	private ProgressDialog _dialog;

	public MatchInfoActivity() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_matchinfo);

		// retrieve information from last activity
		Bundle extras = getIntent().getExtras();
		if (extras != null) {
			_matchId = extras.getString("matchId");
		}
		downloadMatches();

		_userId = app().getUser().getId();

		getWidgets();

		this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
	}

	private void init() {
		_openMatch = app().getUser().getOpenedMatches().get(_matchId);

		_openMatch.getStatus().async(MatchInfoActivity.this);
		_openMatch.getMoves(0).async(MatchInfoActivity.this);
		_openMatch.getChatMessages().async(MatchInfoActivity.this);

		_dialog.dismiss();
	}

	private void getWidgets() {
		layoutMyTurn = (LinearLayout) findViewById(R.id.matchinfo_layout_myTurn);
		layoutTheirTurn = (LinearLayout) findViewById(R.id.matchinfo_layout_theirTurn);
		layoutChat = (LinearLayout) findViewById(R.id.matchinfo_layout_chat);
		player1Name = (TextView) findViewById(R.id.matchinfo_player1);
		player2Name = (TextView) findViewById(R.id.matchinfo_player2);
		score = (TextView) findViewById(R.id.matchinfo_score);
	}

	private void drawPlayerNames() {
		// Left side
		if (checkFirstPlayer()) {
			if (_match.getOpponentId() == null || _match.getOpponentId().isEmpty()) {
				player1Name.setText(app().getUser().getName());
				player2Name.setText("Waiting for Opponent");
			} else {
				player1Name.setText(app().getUser().getName());
				player2Name.setText(_match.getOpponentUsername());
			}
		} else {
			// Right side
			player1Name.setText(_match.getOpponentUsername());
			player2Name.setText(app().getUser().getName());
		}

		// if(_match.get)
		score.setText("Move " + _match.getMoveNum());
	}

	private void drawButtons() {
		drawMyTurnButtons();
		drawTheirTurnButtons();
		drawCommonButtons();
	}

	private void drawMyTurnButtons() {
		if (!_isMyTurn) {
			layoutMyTurn.removeAllViews();
			return;
		}
		Button buttonSelectItem = (Button) findViewById(R.id.matchinfo_select_items);
		buttonSelectItem.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Log.i(TAG, "Select Items:");
				Map<String, Integer> items = new HashMap<String, Integer>();

				_openMatch.seletcItems(items, _match.getMoveNum()).async(MatchInfoActivity.this);
			}
		});

		Button buttonSendMove = (Button) findViewById(R.id.matchinfo_send_move);
		buttonSendMove.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Log.i(TAG, "Send Move:");
				sendMove();
			}
		});

		Button buttonResign = (Button) findViewById(R.id.matchinfo_resign);
		buttonResign.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Log.i(TAG, "Resign:");
				_openMatch.resign().async(MatchInfoActivity.this);
			}
		});

		Button buttonAccept = (Button) findViewById(R.id.matchinfo_accept);
		Button buttonReject = (Button) findViewById(R.id.matchinfo_reject);
		if (!_match.getRandomMatch() && !_match.getClosed() && _match.getMoveNum() == 1
				&& _match.getTurn() == _userId && !_match.getAccepted()) {

			buttonAccept.setOnClickListener(new OnClickListener() {
				public void onClick(View view) {
					Log.i(TAG, "accept:");
					_openMatch.accept().async(MatchInfoActivity.this);
				}
			});

			buttonReject.setOnClickListener(new OnClickListener() {
				public void onClick(View view) {
					Log.i(TAG, "Reject:");
					_openMatch.reject().async(MatchInfoActivity.this);
				}
			});
		} else {
			// remove buttons
			layoutMyTurn.removeView(buttonAccept);
			layoutMyTurn.removeView(buttonReject);
		}

	}

	private void drawTheirTurnButtons() {

		if (_isMyTurn) {
			layoutTheirTurn.removeAllViews();
			return;
		}
		Button buttonSendNudge = (Button) findViewById(R.id.match_sendNudge);
		buttonSendNudge.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Log.i(TAG, "Send Nudge:");
				_openMatch.sendNudge().async(MatchInfoActivity.this);
			}
		});
	}

	private void drawCommonButtons() {

		editTextChatMessage = (EditText) findViewById(R.id.matchinfo_chat_message);

		Button buttonSendChat = (Button) findViewById(R.id.matchinfo_sendchat);
		buttonSendChat.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				Log.i(TAG, "Send Chat:");
				_openMatch.sendChatMessage(_matchId, editTextChatMessage.getText().toString()).async(
						MatchInfoActivity.this);
			}
		});

	}

	private void drawChatMessages() {
		ArrayList<ChatMessage> chatMessages = _openMatch.chatMessages;

		layoutChat.removeAllViews();
		TextView title = new TextView(this);
		title.setText("Chat messages:");
		layoutChat.addView(title);

		if (chatMessages == null)
			return;

		TextView chatText;
		// fill labels
		for (ChatMessage message : chatMessages) {
			chatText = new TextView(this);
			chatText.setText(message.getMsg());
			layoutChat.addView(chatText);
		}
	}

	private void sendMove() {
		HashMap<String, Integer> _rewardItems = new HashMap<String, Integer>();
		HashMap<String, Integer> _attackItems = new HashMap<String, Integer>();
		HashMap<String, Integer> _selectedItems = new HashMap<String, Integer>();

		/*
		 * _rewardItems.put("bomb_01", 1); _rewardItems.put("amazing_powerup",
		 * 1);
		 */
		// _attackItems.put("bomb_01", 1);
		// _attackItems.put("amazing_powerup", 1);
		/*
		 * _selectedItems.put("bomb_01", 1);
		 * _selectedItems.put("amazing_powerup",1);
		 */

		_openMatch.sendMove(_match.getMoveNum(), "test attack for move: " + _match.getMoveNum(),
				_rewardItems, _attackItems, false, _selectedItems).async(MatchInfoActivity.this);
	}

	@Override
	public void onActionMatchAccept(ActionMatchAccept action) {

	}

	@Override
	public void onActionMatchReject(ActionMatchReject action) {

	}

	@Override
	public void onActionMatchResign(ActionMatchResign action) {

	}

	@Override
	public void onActionMatchClose(ActionMatchClose action) {

	}

	@Override
	public void onActionMatchStatusGet(ActionMatchStatusGet action) {
		_match = _openMatch;
		drawPlayerNames();

		drawButtons();
	}

	@Override
	public void onActionMatchSelectItems(ActionMatchSelectItems action) {

	}

	@Override
	public void onActionMatchMoves(ActionMatchMoves action) {

	}

	@Override
	public void onActionMatchSendMove(ActionMatchSendMove action) {

	}

	@Override
	public void onActionChatMessageGet(ActionChatMessageGet action) {		
		drawChatMessages();
	}

	@Override
	public void onActionChatMessageSend(ActionChatMessageSend action) {
		_openMatch.getChatMessages().async(MatchInfoActivity.this);
	}

	@Override
	public void onActionMatchSendNudge(ActionMatchSendNudge action) {

	}

	public boolean checkFirstPlayer() {
		int moveNumber = _match.getMoveNum();
		// Check if its current user turn
		if (_match.getTurn() == null || _match.getTurn().isEmpty() && _match.getRandomMatch())
			_isMyTurn = false;
		else
			_isMyTurn = _match.getTurn().equals(_userId);

		// Debug.Log(moveNumber % 2);
		if (_match.getResigned()) {
			if (_isMyTurn) {
				if (moveNumber % 2 == 1 && _openMatch.getResignedId() != _userId) {
					_mystart = true;
				}
			} else {
				if (moveNumber % 2 == 0 && _openMatch.getResignedId() == _userId) {
					_mystart = true;
				}
			}
		} else {
			if (_match.getClosed())
				moveNumber = 6;
			if (_isMyTurn) {
				if (moveNumber % 2 == 0)
					_mystart = true;
			} else {
				if (moveNumber % 2 == 1)
					_mystart = true;
			}
		}

		return _mystart;
	}

	private void downloadMatches() {
		app().getUser().getOpenedMatches().get().async(MatchInfoActivity.this);

		_dialog = ProgressDialog.show(MatchInfoActivity.this, "", "Loading. Please wait...", true);
	}

	@Override
	public void onActionMatchCurrentListGet(ActionOpenMatchMap action) {
		init();
	}
}
