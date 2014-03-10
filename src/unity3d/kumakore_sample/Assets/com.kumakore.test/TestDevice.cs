using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestDevice : TestBase
	{
		// Valid DEVICE_UDID needed to pass register device related tests
		private static string VALID_DEVICE_UDID = "";
        
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
			
#if UNITY_IPHONE
			device.SyncSetDeviceBadge();
			device.AsyncSetDeviceBadge();
#endif
			
			device.AsyncUnregisterDevice();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
			app().getUser().device ().register (VALID_DEVICE_UDID).sync ();
		}
		
		
		[Test]
        public void SyncMuteDevice()
        {
            app().getUser().device().mute().sync (delegate(ActionDeviceMute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncMuteDevice()
        {
            app().getUser().device().mute().async (delegate(ActionDeviceMute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUnmuteDevice()
        {
            app().getUser().device().unmute().sync (delegate(ActionDeviceUnmute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncUnmuteDevice()
        {
            app().getUser().device().unmute().async (delegate(ActionDeviceUnmute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncRegisterDevice()
        {
            app().getUser().device().register(VALID_DEVICE_UDID).async (delegate(ActionDeviceRegister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUnregisterDevice()
        {
            app().getUser().device().unregister().sync (delegate(ActionDeviceUnregister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncUnregisterDevice()
        {
            app().getUser().device().unregister().async (delegate(ActionDeviceUnregister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
#if UNITY_IPHONE
		[Test]
        public void SyncSetDeviceBadge()
        {
            app().getUser().device().setDeviceBadge(1).sync (delegate(ActionDeviceSetBadge action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncSetDeviceBadge()
        {
            app().getUser().device().setDeviceBadge(2).async (delegate(ActionDeviceSetBadge action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
#endif
	}
}