using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;
using System.Security.Cryptography;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestApp : TestBase
    {	
		private static readonly String TEST_DATA = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()`~[]{}";

		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin(TEST_1, PASSWORD).sync ();
		}

		[Test]
		public void LoadSave()
		{

		}
		
		[Test]
		public void EncryptDecrypt()
		{ 	
			using (RijndaelManaged rijndael = new RijndaelManaged())
			{
				rijndael.GenerateKey();
				rijndael.GenerateIV();

				string encrypted = KumakoreUtil.encryptAES(TEST_DATA, rijndael.Key, rijndael.IV);
				string unencrypted = KumakoreUtil.decryptAES(encrypted, rijndael.Key, rijndael.IV);

				Assert.AreEqual(TEST_DATA, unencrypted, "TEST_DATA should match unencrypted");
        	}
		}

		[Test]
        public void SyncPlatform()
        {
			Assert.AreEqual(StatusCodes.SUCCESS, app1().platform ().sync ());
        }

        [Test]
        public void AsyncPlatform()
        {
            app1().platform().async(delegate(ActionAppPlatform action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncLogInfo()
        {
			Assert.AreEqual(StatusCodes.SUCCESS, app1().logInfo("Sync: Info").sync());
        }

        [Test]
        public void SyncLogDebug()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app1().logDebug("Sync: Debug").sync());
        }

        [Test]
        public void SyncLogWarning()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app1().logWarning("Sync: Warning").sync());
        }

        [Test]
        public void SyncLogError()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app1().logError("Sync: Error").sync());
        }

        [Test]
        public void SyncLogCritical()
        {
            Assert.AreEqual(StatusCodes.SUCCESS, app1().logCritical("Sync: Critical").sync());
        }

        [Test]
        public void AsyncLogInfo()
        {
            app1().logInfo("Async: Info").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogDebug()
        {
            app1().logDebug("Async: Debug").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogWarning()
        {
            app1().logWarning("Async: Warning").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogError()
        {
            app1().logError("Async: Error").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncLogCritical()
        {
            app1().logCritical("Async: Critical").async(delegate(ActionAppLog action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncSignupViaEmail()
        {
            String email = "email_sync_" + Guid.NewGuid().ToString().Substring(0,10) + "@email.com";
            Assert.AreEqual(StatusCodes.SUCCESS, app1().signup(email).sync());
        }

        [Test]
        public void AsyncSignupViaEmail()
        {
            String email = "email_async_" + Guid.NewGuid().ToString().Substring(0,10) + "@email.com";
            app1().signup(email).async(delegate(ActionUserSignup action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncSignupViaUserName()
        {
            String userName = "username_sync_" + Guid.NewGuid().ToString().Substring(0,10);
            Assert.AreEqual(StatusCodes.SUCCESS, app1().signup(userName).sync());
        }

        [Test]
        public void AsyncSignupViaUserName()
        {
            String userName = "username_async_" + Guid.NewGuid().ToString().Substring(0,10);
            app1().signup(userName).async(delegate(ActionUserSignup action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void SyncGetUserId()
        {
            app1().getUserId(TEST_1).sync(delegate(ActionUserGetUserId action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.IsFalse(String.IsNullOrEmpty(action.getUserId()), "userId is null or empty");
            });
        }

        [Test]
        public void AsyncGetUserId()
        {
            app1().getUserId(TEST_1).async(delegate(ActionUserGetUserId action)
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
            Assert.AreEqual(StatusCodes.SUCCESS, app1().passwordReset(TEST_1_EMAIL).sync());
        }

        [Test]
        public void AsyncPasswordReset()
        {
            app1().passwordReset(TEST_1_EMAIL).async(delegate(ActionUserPasswordReset action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
		public void SyncSignin() {
			app1 ().signin (TEST_1_EMAIL,PASSWORD).sync (delegate(ActionUserSignin action) {
				Assert.AreEqual(StatusCodes.SUCCESS,action.getCode());
			});
		}
		
		[Test]
		public void AsyncSignin() {
			app1 ().signin (TEST_1_EMAIL,PASSWORD).async (delegate(ActionUserSignin action) {
				Assert.AreEqual(StatusCodes.SUCCESS,action.getCode());
				Release ();
			});
			Wait ();
		}
		
		[Test]
        public void SyncFacebookConnectAccount()
        {
			if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				app1().facebookConnect(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookConnect action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	            });
			}
        }

        [Test]
        public void AsyncFacebookConnectAccount()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				app1().facebookConnect(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookConnect action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	                Release();
	            });

            	Wait();
			}
		}
		
		[Test]
        public void SyncFacebookLogin()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				app1().facebookLogin(TEST_FACEBOOK_TOKEN).sync(delegate(ActionFacebookSignin action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	            });
			}
        }

        [Test]
        public void AsyncFacebookLogin()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				app1().facebookLogin(TEST_FACEBOOK_TOKEN).async(delegate(ActionFacebookSignin action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	                Release();
	            });

            	Wait();
			}
		}
		
		[Test]
        public void SyncFacebookDeauthorize()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) 
				Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				app1().facebookDeauthorize().sync(delegate(ActionFacebookDeauthorize action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	            });
			}
        }

        [Test]
        public void AsyncFacebookDeauthorize()
        {
            if(String.IsNullOrEmpty(TEST_FACEBOOK_TOKEN)) 
				Assert.Pass("Insert valid TEST_FACEBOOK_TOKEN to test facebook functions");
            else {
				app1().facebookDeauthorize().async(delegate(ActionFacebookDeauthorize action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	                Release();
	            });
	
	            Wait();
			}
		}
		
		[Test]
        public void SyncGetAppRewards()
        {
            app1().getRewards().get ().sync(delegate(ActionAppGetRewardMap action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetAppRewards()
        {
            app1().getRewards().get().async(delegate(ActionAppGetRewardMap action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}

		[Test]
		public void SyncGetAppObject()
		{
            app1().getAppObjects().get("537331b0c8c48f1a4c000175").sync(delegate(ActionAppDatastore action) {
				AppDatastore obj = app1().getAppObjects();
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual("poop", obj["537331b0c8c48f1a4c000175"].getData()["blah"]);
			});
		}

		[Test]
		public void SyncGetAllObjects()
		{
            app1().getAppObjects().get().sync(delegate(ActionAppDatastore action) {
				AppDatastore obj = app1().getAppObjects();
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.AreEqual("nice", obj["5373c9f25481ed2f38000175"].getData()["please"]);
                Assert.AreEqual("poop", obj["537331b0c8c48f1a4c000175"].getData()["blah"]);
			});
		}

        [Test]
        public void AsyncGetAppObject()
        {
            app1().getAppObjects().get("537331b0c8c48f1a4c000175").async(delegate(ActionAppDatastore action) {
                AppDatastore obj = app1().getAppObjects();
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.AreEqual("poop", obj["537331b0c8c48f1a4c000175"].getData()["blah"]);

                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncGetAllObjects()
        {
            app1().getAppObjects().get().sync(delegate(ActionAppDatastore action) {
                AppDatastore obj = app1().getAppObjects();
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Assert.AreEqual("nice", obj["5373c9f25481ed2f38000175"].getData()["please"]);
                Assert.AreEqual("poop", obj["537331b0c8c48f1a4c000175"].getData()["blah"]);

                Release();
            });

            Wait();
        }
    }
}