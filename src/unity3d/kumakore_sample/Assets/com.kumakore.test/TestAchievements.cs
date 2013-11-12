using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestAchievements : TestBase
    {
		public static void All()
        {
            TestAchievements app = new TestAchievements();
            app.setup();
			
			// app achievements
			app.SyncGetAchievementList();
			app.AsyncGetAchievementList();
			
			// user achievements
			app.SyncGetUserAchievementList();
			app.AsyncGetUserAchievementList();
			app.SyncSetUserAchievement();
			app.AsyncSetUserAchievement();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
		}
		
		[Test]
        public void SyncGetAchievementList()
        {
            app().achievements().get().sync(delegate(ActionAppAchievementListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncGetAchievementList()
        {
            app().achievements().get().async(delegate(ActionAppAchievementListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetUserAchievementList()
        {
            app().user ().achievements().get().sync(delegate(ActionUserAchievementListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncGetUserAchievementList()
        {
            app().user().achievements().get().async(delegate(ActionUserAchievementListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSetUserAchievement()
        {
			AppAchievement achievement = app ().achievements()[0];
			app().user().achievements().set (achievement.getName (),(Double)1.0).sync(delegate(ActionUserAchievementSet action)
            {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncSetUserAchievement()
        {
            AppAchievement achievement = app ().achievements()[0];
			app().user().achievements().set(achievement.getName(),(Double)2.0).async(delegate(ActionUserAchievementSet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
	}
}