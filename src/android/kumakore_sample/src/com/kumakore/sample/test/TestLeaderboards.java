package com.kumakore.sample.test;

import com.kumakore.KumakoreApp;

public class TestLeaderboards extends TestBase {
	private static final String TAG = TestLeaderboards.class.getName();

	protected TestLeaderboards(KumakoreApp app) {
		super(app);
	}
	
	@Override
	protected void onRun() {
		testLeaderboards();			
	}
	
	public void testLeaderboards() {
		// ## example 10 get leaderboard ##

		
		
		// Leaderboard leaderboard;]
		// leaderboards == LeaderboardList
		// Get application leaderboards
		// ArrayList<Leaderboard> leaderboards =
		// app.leaderboards().get().sync();
		// TODO:chbfiv commented out because null ptr exception
		// app.leaderboards().get().async(new GetLeaderboardList());

		// Get leaderboard
		// LeaderboardMember[] members = leaderboards[0].get(start, end).sync();

		// Get leaderboard centered on user
		// LeaderboardMember[] members =
		// leaderboards[0].ceneteredOnUser(count).sync();

		// Get leaderboard for user and friends
		// LeaderboardMember[] members = leaderboards[0].friends().sync();

		// Get user rank on leaderboard
		// LeaderboardMember[] members = leaderboards[0].rank().sync();

		// Set user score on leaderboard
		// LeaderboardMember member = leaderboards[0].score().sync();
	}

}
