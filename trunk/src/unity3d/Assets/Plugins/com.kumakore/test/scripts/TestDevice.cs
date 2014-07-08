using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestDevice : TestBase
	{
		/*
		// Valid DEVICE_UDID needed to pass register device related tests
		private static string VALID_DEVICE_UDID = "";

		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin(TEST_1,PASSWORD).sync ();
		}


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
		
		[Test]
        public void SyncMuteDevice()
        {
            app1().getUser().device().mute().sync (delegate(ActionDeviceMute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncMuteDevice()
        {
            app1().getUser().device().mute().async (delegate(ActionDeviceMute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncUnmuteDevice()
        {
            app1().getUser().device().unmute().sync (delegate(ActionDeviceUnmute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncUnmuteDevice()
        {
            app1().getUser().device().unmute().async (delegate(ActionDeviceUnmute action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

        [Test]
        public void AsyncRegisterDevice()
        {
			if(String.IsNullOrEmpty(VALID_DEVICE_UDID)) Assert.Pass("Insert valid VALID_DEVICE_UDID to test register device functions");
            else {
				app1().getUser().device().register(VALID_DEVICE_UDID).async (delegate(ActionDeviceRegister action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	                Release();
	            });
	
	            Wait();
			}
        }
		
		[Test]
        public void SyncRegisterDevice()
        {
			if(String.IsNullOrEmpty(VALID_DEVICE_UDID)) Assert.Pass("Insert valid VALID_DEVICE_UDID to test register device functions");
            else {
				app1().getUser().device().register(VALID_DEVICE_UDID).sync (delegate(ActionDeviceRegister action)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
	            });
			}
        }
		
		[Test]
        public void SyncUnregisterDevice()
        {
            app1().getUser().device().unregister().sync (delegate(ActionDeviceUnregister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncUnregisterDevice()
        {
            app1().getUser().device().unregister().async (delegate(ActionDeviceUnregister action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }

		[Test]
        public void SyncSetDeviceBadge()
        {
            app1().getUser().device().setDeviceBadge(1).sync (delegate(ActionDeviceSetBadge action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncSetDeviceBadge()
        {
            app1().getUser().device().setDeviceBadge(2).async (delegate(ActionDeviceSetBadge action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }*/
	}
}