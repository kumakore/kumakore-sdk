using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
	[TestFixture]
	public class TestGameCenter : TestBase
	{
//		private static string FAKE_EMAIL = "newemail@carlostest.com";
//		private static string FAKE_PASSWORD = "pass";

		public static void All()
		{
			TestGameCenter gc = new TestGameCenter();
			gc.setup();

			gc.SyncGameCenterSignup();
			gc.SyncGameCenterConnect();
		}

		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
		}


		[Test]
		public void SyncGameCenterSignup()
		{
			String cert_url = "https://sandbox.gc.apple.com/public-key/gc-sb.cer";
			String player_id = "G:1659698280";
			long timestamp = 1393824070810;
			String salt = "niEpGQ==";
			String signature = "XTkw9b1KJh2FLQmoqboac9CEFbWoiz8jA9In6qvd0tq+3uP3O2r7He9Kh/SaVfGSwbQbdtk4zsgaJzxUhHbiOVV5fm/YMEmO9m3g65iTG5x99W1wAJywEKsDL06B1HZTzxw+4caW9SZhXreyAWq6rV/77OHu4DIjL9wS/+xVrqGOZtjOj0+N0ZbqFHbUi+0DZvevJFmlGxS/hYOvEypXbJyq3BiOl7HE2R5cugbE7vzAoBThc1J2gyylMne1lRnUv5g83peV0TfRNUHz6u2aIngTdSgAps5vD7Y0wkv89v1MwBEZ/v//B5uvX6wwbBjK2LgknELxWnHWEC+WWEJhfw==";
			String bundle_id = "com.kumakore.gamecentertest";

			app1 ().gamecenterLogin (cert_url, player_id, timestamp, salt, signature, bundle_id).sync (delegate(ActionGameCenterSignin action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.IsNotNullOrEmpty(app1().getUser().getGameCenterId());
				Assert.IsNotNullOrEmpty(app1().getUser().getName());
			});
		}

		[Test]
		public void SyncGameCenterConnect()
		{
			app1().getUser().signout().sync();

			app1().signin(TestBase.TEST_2_EMAIL, TestBase.PASSWORD).sync();

			String cert_url = "https://sandbox.gc.apple.com/public-key/gc-sb.cer";
			String player_id = "G:1534993134";
			long timestamp = 1393824276562;
			String salt = "2D2W3Q==";
			String signature = "LiztPKUZVq3Gs5lIzyfCXJ/8jf29KifHGJ6cDWZX/ComdfFKcssghkowYLPMlJTgUCHyFIVd/ktwSMH8SGmGNmny2951EdudrPgqQsq8zQWYket13QZ/nMlyeleSRYw/5ey+pj5RUwFa3x4sUIl0CP/etUxIy7qv5Hu3G/n+BzCuY6O61rx9nQ5zmGCot3y30Khv3G+5EqKZGhg5uIh/0bvcfht/wnm3dPIwYef3BMBh1atsNmBHBYQpPWdMvNBsSPyb3v4Gl4n/0w+OBzHcHzNEhc7JX23CZA3d55fHBQlXjiVs3Pe3GGCb5+AngmbSXjM/US0fVmdW9lmc1rypYg==";
			String bundle_id = "com.kumakore.gamecentertest";

			app1 ().gamecenterLogin (cert_url, player_id, timestamp, salt, signature, bundle_id).sync (delegate(ActionGameCenterSignin action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.IsNotNullOrEmpty(app1().getUser().getGameCenterId());
				Assert.IsNotNullOrEmpty(app1().getUser().getName());
			});
		}
	}
}