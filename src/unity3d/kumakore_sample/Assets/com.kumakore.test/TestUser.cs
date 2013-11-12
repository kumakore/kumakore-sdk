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
			user.SyncUpdateInfo();
			user.AsyncUpdateInfo();
			
			user.SyncGetFacebookFriends();
			user.AsyncGetFacebookFriends();
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncUserGet()
        {
            app().user().get().sync(delegate(ActionUserGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncUserGet()
        {
            app().user().get().async(delegate(ActionUserGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUpdateInfo()
        {
            app().user().update("carlos_3","kiffen1011@h.com","pass").sync(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncUpdateInfo()
        {
            app().user().update("carlos_2","kiffen1011@hotmail.com","password").async(delegate(ActionUserUpdate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetFacebookFriends()
        {
            app().user().getFacebookFriends().get(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookGetFriends action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncGetFacebookFriends()
        {
            app().user().getFacebookFriends().get(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookGetFriends action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
	}
}