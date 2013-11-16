using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestLeaderboards : TestBase
    {
		public static void All()
        {
            TestLeaderboards app = new TestLeaderboards();
            app.setup();
			
			// get leaderboards
			app.SyncGetLeaderboardList();
			app.AsyncGetLeaderboardList();
			app.SyncGetLeaderboardCenteredOnUser();
			app.AsyncGetLeaderboardCenteredOnUser();
			app.SyncGetLeaderboardRange();
			app.AsyncGetLeaderboardRange();
			app.SyncGetLeaderboardFriends();
			app.AsyncGetLeaderboardFriends();
			app.SyncGetUserRank();
			app.AsyncGetUserRank();
			
			app.SyncSetUserScore();
			app.AsyncSetUserScore();
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
		}
		
		[Test]
        public void SyncGetLeaderboardList()
        {
            app().leaderboards().get().sync(delegate(ActionLeaderboardListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardList()
        {
            app().leaderboards().get().async(delegate(ActionLeaderboardListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetLeaderboardCenteredOnUser()
        {
            app().leaderboards()[0].getMembsCenteredOnUser(3).sync(delegate(ActionLeaderboardMemberListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardCenteredOnUser()
        {
            app().leaderboards()[0].getMembsCenteredOnUser(3).async(delegate(ActionLeaderboardMemberListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetLeaderboardRange()
        {
            app().leaderboards()[0].getMembsGivenRange(0,3).sync(delegate(ActionLeaderboardMemberListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardRange()
        {
            app().leaderboards()[0].getMembsGivenRange(0,3).async(delegate(ActionLeaderboardMemberListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetLeaderboardFriends()
        {
            app().leaderboards()[0].getFriends().sync(delegate(ActionLeaderboardMemberListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetLeaderboardFriends()
        {
            app().leaderboards()[0].getFriends().async(delegate(ActionLeaderboardMemberListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetUserRank()
        {
            app().leaderboards()[0].getUserRank().sync(delegate(ActionUserRankOnLeaderboard action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetUserRank()
        {
            app().leaderboards()[0].getUserRank().async(delegate(ActionUserRankOnLeaderboard action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncSetUserScore()
        {
            app().leaderboards()[0].setUserScore(1).sync(delegate(ActionSetUserScore action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncSetUserScore()
        {
            app().leaderboards()[0].setUserScore(2).async(delegate(ActionSetUserScore action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
	}
}