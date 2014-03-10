using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestInventory : TestBase
	{
		// valid item name
		private static readonly String ITEM_NAME = "b1";
		
        public static void All()
        {
            TestInventory inventory = new TestInventory();
            inventory.setup();
			
			inventory.SyncAddItem();
			inventory.SyncRemoveItem();
			inventory.AsyncAddItem();
			inventory.AsyncRemoveItem();
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncAddItem()
        {
			int quantity = 1;
            app().getUser().getInventory().addItem(ITEM_NAME,quantity).sync(delegate(ActionInventoryAdd action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app ().getUser ().getInventory()[ITEM_NAME].getQuantity(),quantity);
            });
        }

        [Test]
        public void AsyncAddItem()
        {
			int quantity = 2;
            app().getUser().getInventory().addItem(ITEM_NAME,quantity).async(delegate(ActionInventoryAdd action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app ().getUser ().getInventory()[ITEM_NAME].getQuantity(),quantity);
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncRemoveItem()
        {
			int quantity = 0;
			if(app().getUser ().getInventory().ContainsKey(ITEM_NAME)) quantity = app ().getUser().getInventory()[ITEM_NAME].getQuantity();
			else Assert.Pass (ITEM_NAME + " not found in inventory");
            app().getUser().getInventory().removeItem(ITEM_NAME,quantity).sync(delegate(ActionInventoryRemove action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.True(!app ().getUser().getInventory().ContainsKey(ITEM_NAME) || app ().getUser().getInventory()[ITEM_NAME].getQuantity() == 0); 
            });
        }

        [Test]
        public void AsyncRemoveItem()
        {
            int quantity = 0;
			if(app().getUser ().getInventory().ContainsKey(ITEM_NAME)) quantity = app ().getUser().getInventory()[ITEM_NAME].getQuantity();
            else Assert.Pass (ITEM_NAME + " not found in inventory");
            app().getUser().getInventory().removeItem(ITEM_NAME,quantity).async(delegate(ActionInventoryRemove action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.True(!app ().getUser().getInventory().ContainsKey(ITEM_NAME) || app ().getUser().getInventory()[ITEM_NAME].getQuantity() == 0);
                Release();
            });

            Wait();
        }
	}
}