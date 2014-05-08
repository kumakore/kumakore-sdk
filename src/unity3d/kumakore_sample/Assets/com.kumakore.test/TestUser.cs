using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestUser : TestBase
	{
		private static string FAKE_EMAIL = "newemail@carlostest.com";
		private static string FAKE_PASSWORD = "pass";
		
        public static void All()
        {
            TestUser user = new TestUser();
            user.setup();
			
			user.SyncUserGet();
			user.AsyncUserGet();
			user.SyncUpdateInfo();
			user.AsyncUpdateInfo();
			
			user.SyncGetFacebookFriends();
			user.AsyncGetFacebookFriends();
			user.SyncSignOut();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin(TEST_1,PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncUserGet()
        {
            if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
            app1().getUser().get().sync(delegate(ActionUserGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.IsNotNullOrEmpty(app1().getUser().getEmail());
				Assert.IsNotNullOrEmpty(app1().getUser().getName());
            });
        }

        [Test]
        public void AsyncUserGet()
        {
            if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
            app1().getUser().get().async(delegate(ActionUserGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.IsNotNullOrEmpty(app1().getUser().getEmail());
				Assert.IsNotNullOrEmpty(app1().getUser().getName());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUpdateInfo()
        {
			if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
            app1().getUser().update(TEST_1,FAKE_EMAIL,FAKE_PASSWORD).sync(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual(FAKE_EMAIL,app1().getUser().getEmail());
            });
			app1().getUser().update(TEST_1,TEST_1_EMAIL,PASSWORD).sync(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual(TEST_1_EMAIL,app1().getUser().getEmail());
            });
        }

        [Test]
        public void AsyncUpdateInfo()
        {
            if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
            app1().getUser().update(TEST_1,FAKE_EMAIL,FAKE_PASSWORD).async(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.AreEqual(FAKE_EMAIL,app1().getUser().getEmail());
				Release();
            });

            Wait();
			
			app1().getUser().update(TEST_1,TEST_1_EMAIL,PASSWORD).async(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.AreEqual(TEST_1_EMAIL,app1().getUser().getEmail());
				Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetFacebookFriends()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
	            app1().getUser().getFacebookFriends().get(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookGetFriends action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	            });
			}
        }

        [Test]
        public void AsyncGetFacebookFriends()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
	            app1().getUser().getFacebookFriends().get(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookGetFriends action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	                Release();
	            });
	
	            Wait();
			}
        }
		
		[Test]
        public void SyncSignOut()
        {
            if(String.IsNullOrEmpty(app1 ().getUser().getId())) setup ();
            app1().getUser().signout ().sync(delegate(ActionUserSignout action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }
		
	}
}