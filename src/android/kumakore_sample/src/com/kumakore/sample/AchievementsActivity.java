package com.kumakore.sample;

import java.util.ArrayList;
import java.util.List;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.SeekBar;
import android.widget.SeekBar.OnSeekBarChangeListener;
import android.widget.Toast;

import com.kumakore.ActionAchievementGetApp;
import com.kumakore.ActionUserAchievementListGet;
import com.kumakore.ActionUserAchievementSet;
import com.kumakore.AppAchievement;
import com.kumakore.AppAchievementMap;
import com.kumakore.StatusCodes;
import com.kumakore.UserAchievement;
import com.kumakore.UserAchievementMap;
import com.kumakore.listactivity.KumakoreArrayAdapter;

public class AchievementsActivity extends KumakoreSessionActivity implements ActionUserAchievementListGet.IKumakore,
		ActionUserAchievementSet.IKumakore, ActionAchievementGetApp.IKumakore {
	private static final String TAG = AchievementsActivity.class.getName();

	private ListView listViewAchievements;
	private KumakoreArrayAdapter achievementsAdapter;
	private SeekBar achievementAmount;
	Button btAddProgress;

	private AppAchievementMap _appAchievements;
	private UserAchievementMap _userAchievements;

	private AppAchievement _selectedAchievement;

	private boolean userAchievementsDownloaded, appAchievementsDownloaded;

	private ProgressDialog _dialog;

	public AchievementsActivity() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_achievements);

		getWidgets();
		drawButtons();

		updateView();
	}

	private void getWidgets() {
		listViewAchievements = (ListView) findViewById(R.id.achv_listAchievements);

		achievementAmount = (SeekBar) findViewById(R.id.seekBarAchievement);
	}

	private void drawButtons() {

		btAddProgress = (Button) findViewById(R.id.buttonAddProgress);
		btAddProgress.setOnClickListener(new OnClickListener() {
			public void onClick(View view) {
				changeProgress();
			}
		});

		achievementAmount.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {

			@Override
			public void onStopTrackingTouch(SeekBar seekBar) {

			}

			@Override
			public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
				btAddProgress.setText("Set " + progress + "% Progress");
			}

			@Override
			public void onStartTrackingTouch(SeekBar seekBar) {

			}
		});
	}

	private void updateView() {
		userAchievementsDownloaded = appAchievementsDownloaded = false;

		_dialog = ProgressDialog.show(AchievementsActivity.this, "", "Loading. Please wait...", true);

		app().getUser().achievements().get().async(AchievementsActivity.this);
		app().getAchievements().get().async(AchievementsActivity.this);

	}

	private void checkDownload() {
		if (userAchievementsDownloaded && appAchievementsDownloaded) {
			fillAchievementsList();
			_dialog.dismiss();
		}

	}

	private void changeProgress() {

		if (_selectedAchievement == null) {
			Toast toast = Toast.makeText(this, "Select an achievement", Toast.LENGTH_SHORT);
			toast.show();
		}

		UserAchievement userAchivement = _userAchievements.findAchievement(_selectedAchievement.getName());
		if (userAchivement != null) {

			if (userAchivement.getProgress() >= achievementAmount.getProgress()) {
				Toast toast = Toast.makeText(this, "Progress should increase", Toast.LENGTH_SHORT);
				toast.show();
				return;
			}
		}
		Double progress = (double) achievementAmount.getProgress();
		app().getUser().achievements().set(_selectedAchievement.getName(), progress)
				.async(AchievementsActivity.this);
	}

	@Override
	public void onActionUserAchievmentSet(ActionUserAchievementSet action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			updateView();
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.show();
		}
	}

	@Override
	public void onActionUserAchievmentListGet(ActionUserAchievementListGet action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			_userAchievements = app().getUser().achievements();
			userAchievementsDownloaded = true;
			checkDownload();
			// Log.i(TAG, "User Achievements size " + _userAchievements.size());
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.show();
		}

	}

	@Override
	public void onActionAppAchievmentListGet(ActionAchievementGetApp action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			_appAchievements = app().getAchievements();
			appAchievementsDownloaded = true;
			checkDownload();
		} else {
			Toast toast = Toast.makeText(this, action.getStatusMessage(), Toast.LENGTH_SHORT);
			toast.show();
		}
	}

	private void fillAchievementsList() {

		List<String> titles = new ArrayList<String>();
		List<String> descriptions = new ArrayList<String>();
		UserAchievement userAchivement;
		for (AppAchievement achievement : _appAchievements.values()) {
			userAchivement = _userAchievements.findAchievement(achievement.getName());
			if (userAchivement != null)
				titles.add(achievement.getTitle() + " " + userAchivement.getProgress() + "%");
			else
				titles.add(achievement.getTitle() + " 0.0%");

			descriptions.add(achievement.getDescription());
		}

		// View view = findViewById(android.R.layout.activity_list_item);
		achievementsAdapter = new KumakoreArrayAdapter(this, titles, descriptions);

		listViewAchievements.setAdapter(achievementsAdapter);
		listViewAchievements.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
		listViewAchievements.setFocusableInTouchMode(true);

		// handle click on match
		listViewAchievements.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			public void onItemClick(AdapterView<?> parentAdapter, View view, int position, long id) {
				Log.i(TAG, "position " + position);

				_selectedAchievement = _appAchievements.get(position);
				listViewAchievements.setSelection(position);
				achievementsAdapter.setHighlighted(position);
				achievementsAdapter.notifyDataSetChanged();
				UserAchievement uachie = _userAchievements.findAchievement(_selectedAchievement.getName());
				if (uachie != null) {
					int p = uachie.getProgress().intValue();
					achievementAmount.setProgress(p);
				}
			}
		});

	}
}
