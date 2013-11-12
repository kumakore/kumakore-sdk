﻿using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestInventory : TestBase
	{
	
		private static readonly String ITEM_NAME = "b1";
        public static void All()
        {
            TestInventory inventory = new TestInventory();
            inventory.setup();
			
			inventory.SyncAddItem();
			inventory.AsyncAddItem();
			inventory.SyncRemoveItem();
			inventory.AsyncRemoveItem();
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncAddItem()
        {
            app().user().inventory().addItem(ITEM_NAME,10).sync(delegate(ActionInventoryAdd action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncAddItem()
        {
            app().user().inventory().addItem(ITEM_NAME,2).async(delegate(ActionInventoryAdd action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncRemoveItem()
        {
            app().user().inventory().removeItem(ITEM_NAME,1).sync(delegate(ActionInventoryRemove action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncRemoveItem()
        {
            app().user().inventory().removeItem(ITEM_NAME,2).async(delegate(ActionInventoryRemove action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
	}
}