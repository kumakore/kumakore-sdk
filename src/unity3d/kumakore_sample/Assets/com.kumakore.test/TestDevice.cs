using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestDevice : TestBase
	{
        public static void All()
        {
            TestDevice device = new TestDevice();
            device.setup();
			
			// Device
			device.SyncUnregisterDevice();
			device.AsyncRegisterDevice();
			device.SyncMuteDevice();
			device.SyncUnmuteDevice();
			device.AsyncMuteDevice();
			device.AsyncUnmuteDevice();
			device.SyncSetDeviceBadge();
			device.AsyncSetDeviceBadge();
			device.AsyncUnregisterDevice();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
			app().user ().device ().register ("123456789").sync ();
		}
		
		
		[Test]
        public void SyncMuteDevice()
        {
            app().user().device().mute().sync (delegate(ActionDeviceMute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncMuteDevice()
        {
            app().user().device().mute().async (delegate(ActionDeviceMute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUnmuteDevice()
        {
            app().user().device().unmute().sync (delegate(ActionDeviceUnmute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncUnmuteDevice()
        {
            app().user().device().unmute().async (delegate(ActionDeviceUnmute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncRegisterDevice()
        {
            app().user().device().register("123456789").async (delegate(ActionDeviceRegister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUnregisterDevice()
        {
            app().user().device().unregister().sync (delegate(ActionDeviceUnregister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncUnregisterDevice()
        {
            app().user().device().unregister().async (delegate(ActionDeviceUnregister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSetDeviceBadge()
        {
            app().user().device().setDeviceBadge(1).sync (delegate(ActionDeviceSetBadge action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncSetDeviceBadge()
        {
            app().user().device().setDeviceBadge(2).async (delegate(ActionDeviceSetBadge action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
	}
}