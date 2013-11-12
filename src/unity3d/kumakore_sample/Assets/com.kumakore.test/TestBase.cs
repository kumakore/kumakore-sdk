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
        private KumakoreApp _app;
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private static readonly int MAX_TEST_TIMEOUT = 5000;
        protected static readonly String TEST_USERNAME = "carlos";
		protected static readonly String VALID_TEST_EMAIL = "kiffen1011@hotmail.com";
        protected static readonly String VALID_TEST_PASSWORD = "password";
		protected static readonly String TEST_FACEBOOK_TOKEN = "CAAGJJ0L1ZC0cBAKM7qyqICJLwpExK2KU8m4nZBGCLbUZBVYexfenZCGk1cA2D6nujBenXdX4fJG7E2imo7nJGyE4zeF3psMUiQ0HuSek7hlo7ZAXWZAdw0gzpmxX0ajpIRf4nbNtzVAloBovZAP8xFX6G7yZC9OJsGA4ejcI6leFQPqrHbBIZBSUN";
			
		[TestFixtureSetUp]
        public virtual void setup()
        {
            _app = new KumakoreApp(Helpers.GetApiKey(),
               Helpers.GetDashboardVersion());
			
			// create test account
            //if (app().signup(TEST_EMAIL).sync() == StatusCodes.SUCCESS)
            //{
             //   app().user().update().setName(TEST_USERNAME).sync();
            //}
        }

		protected KumakoreApp app ()
		{
			return _app;
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