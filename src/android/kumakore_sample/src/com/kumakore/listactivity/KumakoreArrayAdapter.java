package com.kumakore.listactivity;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.kumakore.sample.R;

public class KumakoreArrayAdapter extends ArrayAdapter<String> {
	private final Context context;
	private final List<String> titles;
	private final List<String> descriptions;
	
	private int highlightedId = -1;

	public KumakoreArrayAdapter(Context context, List<String> titles, List<String> descriptions) {
		super(context, R.layout.list_item_achievements, titles);
		this.context = context;
		this.titles = titles;
		this.descriptions = descriptions;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View rowView = inflater.inflate(R.layout.list_item_achievements, parent, false);
		
		TextView textView = (TextView) rowView.findViewById(R.id.firstLine);		
		textView.setText(titles.get(position));
		
		textView = (TextView) rowView.findViewById(R.id.secondLine);		
		textView.setText(descriptions.get(position));

		// Change the icon
		ImageView imageView = (ImageView) rowView.findViewById(R.id.icon);
		imageView.setImageResource(R.drawable.kumakore_icon);
		
		if(highlightedId == position)
			rowView.setBackgroundColor(context.getResources().getColor(R.color.list_background_selected));
		else
			rowView.setBackgroundColor(context.getResources().getColor(R.color.list_background_light));
		

		return rowView;
	}
	
	public void setHighlighted(int id){
		highlightedId = id;
	}
	
}
