package com.kumakore.listactivity;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.kumakore.Product;
import com.kumakore.sample.R;

public class ItemArrayAdapter extends ArrayAdapter<String> {
	private final Context context;
	private final List<String> titles;
	private final List<String> descriptions;
	private final List<Product> products;

	private int highlightedId = -1;

	public ItemArrayAdapter(Context context, List<String> titles, List<String> descriptions, List<Product> products) {
		super(context, R.layout.list_item_achievements, titles);
		this.context = context;
		this.titles = titles;
		this.descriptions = descriptions;
		this.products = products;
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

		String type = products.get(position).getCategory();

		if (type == null)
			imageView.setImageResource(R.drawable.kumakore_icon);
		else if (type.equals("coin"))
			imageView.setImageResource(R.drawable.coin);
		else if (type.equals("avatar"))
			imageView.setImageResource(R.drawable.avatar);
		else if (type.equals("powerup"))
			imageView.setImageResource(R.drawable.powerup);
		else if (type.isEmpty())
			imageView.setImageResource(R.drawable.kumakore_icon);

		if (highlightedId == position)
			rowView.setBackgroundColor(context.getResources().getColor(R.color.list_background_selected));
		else
			rowView.setBackgroundColor(context.getResources().getColor(R.color.list_background_light));

		return rowView;
	}

	public void setHighlighted(int id) {
		highlightedId = id;
	}

}
