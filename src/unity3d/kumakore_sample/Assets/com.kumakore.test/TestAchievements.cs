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
		
		public static void All()
        {
            TestAchievements app = new TestAchievements();
            app.setup();
			
			// app achievements
			app.SyncGetAchievementList(NUMBER_OF_ACHIEVEMENTS);
			app.AsyncGetAchievementList(NUMBER_OF_ACHIEVEMENTS);
			
			// user achievements
			app.SyncGetUserAchievementList();
			app.AsyncGetUserAchievementList();
			app.SyncSetUserAchievement(ACHIEVEMENT_1);
			app.AsyncSetUserAchievement(ACHIEVEMENT_1);
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
		}
		
		[Test]
        public void SyncGetAchievementList(int count)
        {
            app().getAchievements().get().sync(delegate(ActionAchievementGetApp action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual (app().getAchievements().Count,count);
            });
        }

        [Test]
        public void AsyncGetAchievementList(int count)
        {
            app().getAchievements().get().async(delegate(ActionAchievementGetApp action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual (app().getAchievements().Count,count);
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetUserAchievementList()
        {
            app().getUser().getAchievements().get().sync(delegate(ActionAchievementGetUser action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetUserAchievementList()
        {
            app().getUser().getAchievements().get().async(delegate(ActionAchievementGetUser action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSetUserAchievement(String achievement)
        {
			Double progress = 1.0;
			foreach(UserAchievement ach in app().getUser().getAchievements().Values) {
				if(achievement.Equals(ach.getName ())) progress += ach.getProgress();
			}
			app().getUser().getAchievements().set (achievement,progress).sync(delegate(ActionAchievementSetUser action)
            {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				foreach(UserAchievement a in app().getUser().getAchievements().Values) {
					if(a.getName () == achievement) Assert.AreEqual(progress,a.getProgress());
				}
            });
        }

        [Test]
        public void AsyncSetUserAchievement(String achievement)
        {
			Double progress = 1.0;
			foreach(UserAchievement ach in app().getUser().getAchievements().Values) {
				if(achievement.Equals(ach.getName ())) progress += ach.getProgress();
			}
			app().getUser().getAchievements().set (achievement,progress).async(delegate(ActionAchievementSetUser action)
            {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				foreach(UserAchievement a in app().getUser().getAchievements().Values) {
					if(a.getName () == achievement) Assert.AreEqual(progress,a.getProgress());
				}
				Release ();
            });
			Wait ();
        }
		
	}
}