using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestApp : TestBase
    {
	    public static void All()
        {
            TestApp app = new TestApp();
            app.setup();
			//// sync log
            app.SyncLogInfo();
            app.SyncLogDebug();
            app.SyncLogWarning();
            app.SyncLogError();
            app.SyncLogCritical();

            //// async log
            app.AsyncLogInfo();
            app.AsyncLogDebug();
            app.AsyncLogWarning();
            app.AsyncLogError();
            app.AsyncLogCritical();

            ////user
            app.SyncSignupViaEmail();
            app.AsyncSignupViaEmail();
            app.SyncSignupViaUserName();
            app.AsyncSignupViaUserName();

            app.SyncGetUserId();
            app.AsyncGetUserId();
            app.SyncPasswordReset();
            app.AsyncPasswordReset();
			
			app.SyncSignin();
			app.AsyncSignin();
			
			app.SyncGetAppRewards();
			app.AsyncGetAppRewards();
			
			app.SyncFacebookConnectAccount();
			app.SyncFacebookLogin();
			app.SyncFacebookDeauthorize();
			app.AsyncFacebookConnectAccount();
			app.AsyncFacebookLogin();
			app.AsyncFacebookDeauthorize();
			
        }
		
		[Test]
        public void SyncLogInfo()
        {
			Assert.AreEqual(StatusCodes.SUCCESS, app().logInfo("Sync: Info").sync());
        }

        [Test]
        public void SyncLogDebug()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app().logDebug("Sync: Debug").sync());
        }

        [Test]
        public void SyncLogWarning()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app().logWarning("Sync: Warning").sync());
        }

        [Test]
        public void SyncLogError()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app().logError("Sync: Error").sync());
        }

        [Test]
        public void SyncLogCritical()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app().logCritical("Sync: Critical").sync());
        }

        [Test]
        public void AsyncLogInfo()
        {
            app().logInfo("Async: Info").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogDebug()
        {
            app().logDebug("Async: Debug").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogWarning()
        {
            app().logWarning("Async: Warning").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogError()
        {
            app().logError("Async: Error").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogCritical()
        {
            app().logCritical("Async: Critical").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncSignupViaEmail()
        {
            String email = "email_sync_" + Guid.NewGuid().ToString() + "@email.com";
            Assert.AreEqual(StatusCodes.SUCCESS, app().signup(email).sync());
        }

        [Test]
        public void AsyncSignupViaEmail()
        {
            String email = "email_async_" + Guid.NewGuid().ToString() + "@email.com";
            app().signup(email).async(delegate(ActionUserSignup action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncSignupViaUserName()
        {
            String userName = "username_sync_" + Guid.NewGuid().ToString();
            Assert.AreEqual(StatusCodes.SUCCESS, app().signup(userName).sync());
        }

        [Test]
        public void AsyncSignupViaUserName()
        {
            String userName = "username_async_" + Guid.NewGuid().ToString();
            app().signup(userName).async(delegate(ActionUserSignup action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncGetUserId()
        {
            app().getUserId(TEST_1).sync(delegate(ActionUserGetUserId action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.IsFalse(String.IsNullOrEmpty(action.getUserId()), "userId is null or empty");
            });
        }

        [Test]
        public void AsyncGetUserId()
        {
            app().getUserId(TEST_1).async(delegate(ActionUserGetUserId action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.IsFalse(String.IsNullOrEmpty(action.getUserId()), "userId is null or empty");
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncPasswordReset()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app().passwordReset(TEST_1_EMAIL).sync());
        }

        [Test]
        public void AsyncPasswordReset()
        {
            app().passwordReset(TEST_1_EMAIL).async(delegate(ActionUserPasswordReset action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
		public void SyncSignin() {
			app ().signin (TEST_1_EMAIL,PASSWORD).sync (delegate(ActionUserSignin action) {
				Assert.AreEqual(StatusCodes.SUCCESS,action.getCode());
			});
		}
		
		[Test]
		public void AsyncSignin() {
			app ().signin (TEST_1_EMAIL,PASSWORD).async (delegate(ActionUserSignin action) {
				Assert.AreEqual(StatusCodes.SUCCESS,action.getCode());
				Release ();
			});
			Wait ();
		}
		
		[Test]
        public void SyncFacebookConnectAccount()
        {
            app().facebookConnect(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookConnect action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncFacebookConnectAccount()
        {
            app().facebookConnect(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookConnect action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncFacebookLogin()
        {
            app().facebookLogin(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookSignin action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncFacebookLogin()
        {
            app().facebookLogin(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookSignin action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncFacebookDeauthorize()
        {
            app().facebookDeauthorize().sync(delegate(ActionFacebookDeauthorize action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncFacebookDeauthorize()
        {
            app().facebookDeauthorize().async(delegate(ActionFacebookDeauthorize action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncGetAppRewards()
        {
            app().getRewards().get ().sync(delegate(ActionAppGetRewardMap action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetAppRewards()
        {
            app().getRewards().get().async(delegate(ActionAppGetRewardMap action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
	}
}