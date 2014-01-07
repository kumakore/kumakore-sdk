using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestUser : TestBase
	{
        public static void All()
        {
            TestUser user = new TestUser();
            user.setup();
			
			user.SyncUserGet();
			user.AsyncUserGet();
			user.SyncUpdateInfo(TEST_1,"newemail@carlostest.com","pass");
			user.AsyncUpdateInfo(TEST_1,TEST_1_EMAIL,PASSWORD);
			
			user.SyncGetFacebookFriends();
			user.AsyncGetFacebookFriends();
			user.SyncSignOut();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncUserGet()
        {
            app().getUser().get().sync(delegate(ActionUserGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					//Assert.IsNotNullOrEmpty(app().getUser().getEmail());
					//Assert.IsNotNullOrEmpty(app().getUser().getName());
            });
        }

        [Test]
        public void AsyncUserGet()
        {
            app().getUser().get().async(delegate(ActionUserGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					//Assert.IsNotNullOrEmpty(app().getUser().getEmail());
					//Assert.IsNotNullOrEmpty(app().getUser().getName());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUpdateInfo(String username, String newEmail, String pass)
        {
            app().getUser().update(username,newEmail,pass).sync(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual(newEmail,app().getUser().getEmail());
            });
        }

        [Test]
        public void AsyncUpdateInfo(String username, String newEmail, String pass)
        {
            app().getUser().update(username,newEmail,pass).async(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.AreEqual(newEmail,app().getUser().getEmail());
				Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetFacebookFriends()
        {
            app().getUser().getFacebookFriends().get(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookGetFriends action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetFacebookFriends()
        {
            app().getUser().getFacebookFriends().get(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookGetFriends action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSignOut()
        {
            app().getUser().signout ().sync(delegate(ActionUserSignout action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }
		
	}
}