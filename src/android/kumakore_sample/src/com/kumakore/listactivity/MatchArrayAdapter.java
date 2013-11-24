package com.kumakore.listactivity;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.kumakore.Match;
import com.kumakore.sample.R;

public class MatchArrayAdapter extends ArrayAdapter<String> {
	private final Context context;
	private final List<Match> matches;

	public MatchArrayAdapter(Context context, List<String> titles, List<Match> matches) {
		super(context, R.layout.list_item_achievements, titles);
		this.context = context;
		this.matches = matches;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View rowView = inflater.inflate(R.layout.list_item_achievements, parent, false);

		Match match = matches.get(position);

		TextView textView = (TextView) rowView.findViewById(R.id.firstLine);
		String info = match.getOpponentUsername();
		if (info == null || info.isEmpty())
			info = "Random Opponent";
		else
			info = "Match with " + match.getOpponentUsername();
		textView.setText(info);

		textView = (TextView) rowView.findViewById(R.id.secondLine);
		textView.setText("Move number " + match.getMoveNum());

		// Change the icon
		ImageView imageView = (ImageView) rowView.findViewById(R.id.icon);
		imageView.setImageResource(R.drawable.kumakore_icon);

		if (match.getCompleted())
			rowView.setBackgroundColor(context.getResources().getColor(R.color.list_background_selected));
		else
			rowView.setBackgroundColor(context.getResources().getColor(R.color.list_match_background_normal));

		return rowView;
	}

	public Match getMatch(int position) {
		return matches.get(position);
	}

	public Match getMatch(String matchId) {
		Match match = null;
		for (Match auxMatch : matches) {
			if (auxMatch.getMatchId().equals(matchId)) {
				match = auxMatch;
				break;
			}
		}

		return match;
	}
}
