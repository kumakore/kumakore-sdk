using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
	[TestFixture]
	public class TestLeaderboards : TestBase
    {
		// Requires a valid and linked TEST_FACEBOOK_TOKEN -see TestBase
		private static readonly string LEADERBOARD_NAME = "leaderboard_1"; // leaderboard ID known to be true for the app
		
		public static void All()
        {
            TestLeaderboards test = new TestLeaderboards();
            test.setup();
			
			test.SyncGetLeaderboardList(); // get initially blank leaderboard
			test.LeaderboardNameExists(LEADERBOARD_NAME); // Check leaderboard received is the right one
			test.SyncSetUserScore(); // Set user score
			
			test.SignIn(TEST_2,PASSWORD);
			test.SyncSetUserScore(); // Set user score
			
			test.SignIn(TEST_3,PASSWORD);
			test.SyncSetUserScore(); // Set user score
			
			test.SyncGetLeaderboardCenteredOnUser();
			test.LeaderboardCount(LEADERBOARD_NAME); // Check a score has been set
			
			test.SyncGetUserRank();
			test.LeaderboardUserMember(LEADERBOARD_NAME,3); // last score should be the user's
			
			// Basic testing for the rest of the calls
			test.AsyncGetLeaderboardList();
			test.SyncGetLeaderboardCenteredOnUser();
			test.AsyncGetLeaderboardCenteredOnUser();
			test.SyncGetLeaderboardRange();
			test.AsyncGetLeaderboardRange();
			test.SyncGetLeaderboardFriends();
			test.AsyncGetLeaderboardFriends();
			test.SyncGetFriendsGivenRange();
			test.AsyncGetFriendsGivenRange();
			test.SyncGetFriendsCenteredOnUser();
			test.AsyncGetFriendsCenteredOnUser();
			test.AsyncGetUserRank();
			test.AsyncSetUserScore();
			test.SyncGetOpponents();
			test.AsyncGetOpponents();
			test.SyncGetFBFriendsGivenRange();
			test.AsyncGetFBFriendsGivenRange();
			test.SyncGetFBFriends();
			test.AsyncGetFBFriends();
			test.SyncGetFBFriendsCenteredOnUser();
			test.AsyncGetFBFriendsCenteredOnUser();
			
        }
		
		public void SignIn(string user, string pass) {
			app ().signin(user,pass).sync ();
		}
		
		public void LeaderboardNameExists(string lName) {
			Assert.IsNotNull(GetLeaderboardByName (lName));
		}
		
		public void LeaderboardCount(string lName) {
			Leaderboard leaderboard = GetLeaderboardByName(lName);
			Assert.GreaterOrEqual(leaderboard.getMembers().Count,1);
		}
		
		public void LeaderboardUserMember(string lName, int index) {
			Leaderboard leaderboard = GetLeaderboardByName(lName);
			if(index < leaderboard.getMembers().Count) Assert.AreEqual(leaderboard.getMembers()[index].getMemberId(),app().getUser().getId());
			else Assert.Fail("index higher than leaderboard.getMembers() count");
			
			Assert.IsNotNull(leaderboard);
		}
		
		public Leaderboard GetLeaderboardByName(String leaderboardName) {
			foreach(Leaderboard l in app().getLeaderboards().Values) {
				if(l.getName () == leaderboardName) return l;
			}
			Assert.Fail("leaderboard Name " + leaderboardName + " not found");
			return null;
		}
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
			SyncGetLeaderboardList();
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
		
		[Test]
        public void SyncGetLeaderboardCenteredOnUser()
        {
			Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getMembsCenteredOnUser(3).sync (delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardCenteredOnUser()
        {
           Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getMembsCenteredOnUser(3).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetLeaderboardRange()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getMembsGivenRange(0,3).sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardRange()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getMembsGivenRange(0,3).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetLeaderboardFriends()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFriends().sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardFriends()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFriends().async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetFriendsGivenRange()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFriendsGivenRange(0,3).sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetFriendsGivenRange()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFriendsGivenRange(0,3).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetFriendsCenteredOnUser()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFriendsCenteredOnUser(3).sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetFriendsCenteredOnUser()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFriendsCenteredOnUser(3).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetUserRank()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getUserRank().sync(delegate(ActionLeaderboardUserRank action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetUserRank()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getUserRank().async(delegate(ActionLeaderboardUserRank action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncSetUserScore()
        {
			Random rnd = new Random();
			int score = rnd.Next(1,100);
			Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.setUserScore(score).sync(delegate(ActionLeaderboardSetScore action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				LeaderboardCount(LEADERBOARD_NAME);
				LeaderboardUserMember(LEADERBOARD_NAME,1);
            });
        }

        [Test]
        public void AsyncSetUserScore()
        {
			Random rnd = new Random();
			int score = rnd.Next(1,100);
			Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.setUserScore(score).async(delegate(ActionLeaderboardSetScore action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				LeaderboardCount(LEADERBOARD_NAME);
				LeaderboardUserMember(LEADERBOARD_NAME,1);
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetOpponents()
        {
			Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getOpponents().sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetOpponents()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getOpponents().async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetFBFriendsGivenRange()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFBFriendsGivenRange(0,3,TEST_FACEBOOK_TOKEN).sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetFBFriendsGivenRange()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFBFriendsGivenRange(0,3,TEST_FACEBOOK_TOKEN).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetFBFriendsCenteredOnUser()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFBFriendsCenteredOnUser(3,TEST_FACEBOOK_TOKEN).sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetFBFriendsCenteredOnUser()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFBFriendsCenteredOnUser(3,TEST_FACEBOOK_TOKEN).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetFBFriends()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFBFriends(TEST_FACEBOOK_TOKEN).sync(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }
		
		
        //[Test]
        public void AsyncGetFBFriends()
        {
            Leaderboard leaderboard = GetLeaderboardByName(LEADERBOARD_NAME);
            leaderboard.getFBFriends(TEST_FACEBOOK_TOKEN).async(delegate(ActionLeaderboardGetMembers action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
	}
}