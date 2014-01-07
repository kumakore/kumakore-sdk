using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
	// Before running the test, reset the leaderboard on the App Console
    [TestFixture]
	public class TestLeaderboards : TestBase
    {
		private static readonly string LEADERBOARD_NAME = "leaderboard_1"; // leaderboard ID known to be true for the app
		
		public static void All()
        {
//            TestLeaderboards test = new TestLeaderboards();
//            test.setup();
//			
//			test.SignIn(TEST_1,PASSWORD);
//			test.SyncGetLeaderboardList(); // get initially blank leaderboard
//			test.LeaderboardID(LEADERBOARD_NAME); // Check leaderboard received is the right one
//			int score = 1;
//			test.SyncSetUserScore(score); // Set user score
//			
//			test.SignIn(TEST_2,PASSWORD);
//			score = 2;
//			test.SyncSetUserScore(score); // Set user score
//			
//			test.SignIn(TEST_3,PASSWORD);
//			score = 3;
//			test.SyncSetUserScore(score); // Set user score
//			
//			test.SyncGetLeaderboardCenteredOnUser();
//			test.LeaderboardCount(LEADERBOARD_NAME,3); // Check all scores have been set
//			
//			test.SyncGetUserRank();
//			test.LeaderboardUserMember(LEADERBOARD_NAME,3); // last score should be the user's
//			
//			// Basic testing for the rest of the calls
//			test.AsyncGetLeaderboardList();
//			test.SyncGetLeaderboardCenteredOnUser();
//			test.AsyncGetLeaderboardCenteredOnUser();
//			test.SyncGetLeaderboardRange();
//			test.AsyncGetLeaderboardRange();
//			test.SyncGetLeaderboardFriends();
//			test.AsyncGetLeaderboardFriends();
//			test.AsyncGetUserRank();
//			test.AsyncSetUserScore(5);
			
        }
		
		public void SignIn(string user, string pass) {
			app ().signin(user,pass).sync ();
		}
		
		public void LeaderboardID(string lName) {
			bool found = false;
			foreach(Leaderboard leaderboard in app ().getLeaderboards().Values) {
				if(leaderboard.getName() == lName) found = true;
			}
			Assert.AreEqual(true,found);
		}
		
		public void LeaderboardCount(string lName, int count) {
			bool found = false;
			foreach(Leaderboard leaderboard in app ().getLeaderboards().Values) {
				if(leaderboard.getName() == lName) {
					found = true;
					Assert.AreEqual(leaderboard.getMembers().Count,count);
				}
			}
			Assert.AreEqual(true,found);
		}
		
		public void LeaderboardUserMember(string lName, int index) {
			bool found = false;
			foreach(Leaderboard leaderboard in app ().getLeaderboards().Values) {
				if(leaderboard.getName() == lName) {
					found = true;
					if(index < leaderboard.getMembers().Count) Assert.AreEqual(leaderboard.getMembers()[index].getMemberId(),app().getUser().getId());
					else Assert.Fail("index higher than leaderboard.getMembers() count");
				}
			}
			Assert.AreEqual(true,found);
		}
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
		}
		
		[Test]
        public void SyncGetLeaderboardList()
        {
            app().getLeaderboards().get().sync(delegate(ActionLeaderboardGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardList()
        {
            app().getLeaderboards().get().async(delegate(ActionLeaderboardGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
//		[Test]
//        public void SyncGetLeaderboardCenteredOnUser()
//        {
////            app().getLeaderboards()[0].getMembsCenteredOnUser(3).sync(delegate(ActionLeaderboardMemberListGet action)
////            {
////                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
////            });
//        }
//
//        [Test]
//        public void AsyncGetLeaderboardCenteredOnUser()
//        {
////            app().getLeaderboards()[0].getMembsCenteredOnUser(3).async(delegate(ActionLeaderboardMemberListGet action)
////            {
////                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
////                Release();
////            });
////
////            Wait();
//		}
//		
//		[Test]
//        public void SyncGetLeaderboardRange()
//        {
//            app().getLeaderboards()[0].getMembsGivenRange(0,3).sync(delegate(ActionLeaderboardMemberListGet action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//            });
//        }
//
//        [Test]
//        public void AsyncGetLeaderboardRange()
//        {
//            app().getLeaderboards()[0].getMembsGivenRange(0,3).async(delegate(ActionLeaderboardMemberListGet action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//                Release();
//            });
//
//            Wait();
//		}
//		
//		[Test]
//        public void SyncGetLeaderboardFriends()
//        {
//            app().getLeaderboards()[0].getFriends().sync(delegate(ActionLeaderboardMemberListGet action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//            });
//        }
//
//        [Test]
//        public void AsyncGetLeaderboardFriends()
//        {
//            app().getLeaderboards()[0].getFriends().async(delegate(ActionLeaderboardMemberListGet action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//                Release();
//            });
//
//            Wait();
//		}
//		
//		[Test]
//        public void SyncGetUserRank()
//        {
//            app().getLeaderboards()[0].getUserRank().sync(delegate(ActionUserRankOnLeaderboard action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//            });
//        }
//
//        [Test]
//        public void AsyncGetUserRank()
//        {
//            app().getLeaderboards()[0].getUserRank().async(delegate(ActionUserRankOnLeaderboard action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//                Release();
//            });
//
//            Wait();
//		}
//		
//		[Test]
//        public void SyncSetUserScore(int score)
//        {
//            app().getLeaderboards()[0].setUserScore(score).sync(delegate(ActionSetUserScore action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//            });
//        }
//
//        [Test]
//        public void AsyncSetUserScore(int score)
//        {
//            app().getLeaderboards()[0].setUserScore(score).async(delegate(ActionSetUserScore action)
//            {
//                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
//                Release();
//            });
//
//            Wait();
//		}
	}
}