using System;
using com.kumakore;
using System.Threading;
using NUnit.Framework;

namespace com.kumakore.test
{
	public class TestBase
	{
#pragma warning disable 0414
        private static readonly String TAG = typeof(TestBase).Name;
#pragma warning restore 0414
		private KumakoreApp _app1;
		private KumakoreApp _app2;
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        private static readonly int MAX_TEST_TIMEOUT = 5000;
		protected static readonly String TEST_FACEBOOK_TOKEN = "";

//		protected static readonly String TEST_1_EMAIL = "test01@carlostest.com";
//      protected static readonly String TEST_1 = "test01"; // Valid test user for the app
//		protected static readonly String TEST_2 = "test02"; // Valid test user for the app
//		protected static readonly String TEST_3 = "test03"; // Valid test user for the app
//		protected static readonly String PASSWORD = "password"; // Valid password for test users
//		protected static readonly String API_KEY = "78ed3add3e4d66b818fbf1abb6689855";
//		protected static readonly int DASHBOARDVERSION = 1386951174;

		protected static readonly String TEST_1_EMAIL = "test01@test.com";
		protected static readonly String TEST_1 = "test01"; // Valid test user for the app
		protected static readonly String TEST_2_EMAIL = "test02@test.com";
		protected static readonly String TEST_2 = "test02"; // Valid test user for the app
		protected static readonly String TEST_3_EMAIL = "test03@test.com";
		protected static readonly String TEST_3 = "test03"; // Valid test user for the app
		protected static readonly String PASSWORD = "password"; // Valid password for test users
		protected static readonly String API_KEY = "292c9e31e5187c58b58f5f8588edc92d";
		protected static readonly int DASHBOARDVERSION = 1399273323;

		[TestFixtureSetUp]
        public virtual void setup()
        {
			_app1= new KumakoreApp(API_KEY,DASHBOARDVERSION);
			
			_app1.getDispatcher().immediateDispatch = true;
			_app1.getDispatcher ().throwEx = true;

			_app2 = new KumakoreApp(API_KEY,DASHBOARDVERSION);

			_app2.getDispatcher().immediateDispatch = true;
			_app2.getDispatcher ().throwEx = true;

			// create test account

//			if (app().signup(TEST_1_EMAIL).sync() == StatusCodes.SUCCESS)
//            {
//				app().getUser().update().setName(TEST_1).setPassword(PASSWORD).sync();
//            }
//			if (app().signup(TEST_2_EMAIL).sync() == StatusCodes.SUCCESS)
//			{
//				app().getUser().update().setName(TEST_2).setPassword(PASSWORD).sync();
//			}
//			if (app().signup(TEST_3_EMAIL).sync() == StatusCodes.SUCCESS)
//			{
//				app().getUser().update().setName(TEST_3).setPassword(PASSWORD).sync();
//			}
        }
		
		protected KumakoreApp app1 ()
		{
			return _app1;
		}

		protected KumakoreApp app2 ()
		{
			return _app2;
		}

        protected void Release()
        {
            _resetEvent.Set();
        }

        protected void Wait()
        {
            Assert.IsTrue(_resetEvent.WaitOne(MAX_TEST_TIMEOUT), "AutoResetEvent timeout " + MAX_TEST_TIMEOUT + " exceeded");
        }
	}
}