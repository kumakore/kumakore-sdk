using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestAchievements : TestBase
    {
		private static readonly String ACHIEVEMENT_1 = "ach_1"; // valid achievement name for app
		private static readonly int NUMBER_OF_ACHIEVEMENTS = 2; // number of achievements on app
		
		// run testcases from Unity scene
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
			app1 ().signin(TEST_1,PASSWORD).sync ();
		}
		
		[Test]
        public void SyncGetAchievementList()
        {
            app1().getAchievements().get().sync(delegate(ActionAchievementGetApp action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual (NUMBER_OF_ACHIEVEMENTS, app1().getAchievements().Count);
            });
        }

        [Test]
        public void AsyncGetAchievementList()
        {
            app1().getAchievements().get().async(delegate(ActionAchievementGetApp action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual (NUMBER_OF_ACHIEVEMENTS, app1().getAchievements().Count);
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetUserAchievementList()
        {
            app1().getUser().getAchievements().get().sync(delegate(ActionAchievementGetUser action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetUserAchievementList()
        {
            app1().getUser().getAchievements().get().async(delegate(ActionAchievementGetUser action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSetUserAchievement()
        {
			Double progress = 1.0;
			foreach(UserAchievement ach in app1().getUser().getAchievements().Values) {
				if(ACHIEVEMENT_1.Equals(ach.getName ())) progress += ach.getProgress();
			}
			app1().getUser().getAchievements().set (ACHIEVEMENT_1,progress).sync(delegate(ActionAchievementSetUser action)
            {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				foreach(UserAchievement a in app1().getUser().getAchievements().Values) {
					if(a.getName () == ACHIEVEMENT_1) Assert.AreEqual(progress,a.getProgress());
				}
            });
        }

        [Test]
        public void AsyncSetUserAchievement()
        {
			Double progress = 1.0;
			foreach(UserAchievement ach in app1().getUser().getAchievements().Values) {
				if(ACHIEVEMENT_1.Equals(ach.getName ())) progress += ach.getProgress();
			}
			app1().getUser().getAchievements().set (ACHIEVEMENT_1,progress).async(delegate(ActionAchievementSetUser action)
            {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				foreach(UserAchievement a in app1().getUser().getAchievements().Values) {
					if(a.getName () == ACHIEVEMENT_1) Assert.AreEqual(progress,a.getProgress());
				}
				Release ();
            });
			Wait ();
        }
		
	}
}