package com.kumakore.listactivity;

import java.util.Date;

import android.content.Context;
import android.graphics.Color;
import android.text.format.DateUtils;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.kumakore.Match;
import com.kumakore.sample.MatchListActivity;
import com.kumakore.sample.R;

public class MatchButton extends RelativeLayout implements OnClickListener {
	private MatchListActivity _matchListActivity;
	private Context _context;
	private Match _match;
	private TextView _opponentName;
	private TextView _info;
	private TextView _date;
	private boolean _myTurn;

	public MatchButton(MatchListActivity matchListActivity, Match match, boolean myTurn) {
		super(matchListActivity);
		_matchListActivity = matchListActivity;
		_match = match;
		_myTurn = myTurn;
		_context = matchListActivity;
		init();
	}

	public MatchButton(Context context, AttributeSet att) {
		super(context, att);
		_context = context;
		init();
	}

	private void init() {
		LayoutInflater inflater;
		inflater = (LayoutInflater) _context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View v = inflater.inflate(R.layout.button_matchlist, this, true);
		v.setClickable(true);
		 v.setOnClickListener(this);

		_opponentName = (TextView) findViewById(R.id.firstLine);
		_info = (TextView) findViewById(R.id.secondLine);
		_date = (TextView) findViewById(R.id.thirdLine);

		// Setup opponent name
		String opponent = _match.getOpponentUsername();
		if (opponent == null || opponent.isEmpty())
			opponent = "Waiting for opponent";
		_opponentName.setText(opponent);

		// match info text
		String infoText;
		if (_myTurn)
			infoText = "Your turn to finish round " + _match.getMoveNum() + ".";
		else
			infoText = "Their turn to finish round " + _match.getMoveNum() + ".";
		_info.setText(infoText);

		// datetext
		String dateText;
		Date dateNow = new Date();
		dateText = "Last move was ";
		dateText += (String) DateUtils.getRelativeTimeSpanString(_match.getUpdatedUTC().getTime(),
				dateNow.getTime(), 0);
		_date.setText(dateText);
		_date.setTextColor(Color.GRAY);
	}

	@Override
	public void onClick(View v) {
		_matchListActivity.RequestMatchStatus(_match);
	}

}
