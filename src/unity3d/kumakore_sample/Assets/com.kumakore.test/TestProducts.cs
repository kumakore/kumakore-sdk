using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestProducts : TestBase
    {
		// TEST_1 user should have enough coins (or items) to pay for VALID_PRODUCT_ID
		private String VALID_PRODUCT_ID = "it1";
		
	    public static void All()
        {
            TestProducts app = new TestProducts();
            app.setup();
			
			// app products
			app.SyncGetProductList();
			app.AsyncGetProductList();
			
			// User products
			app.SyncBuyProduct();
			app.AsyncBuyProduct();
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
		}
		
		[Test]
        public void SyncGetProductList()
        {
            app().getProducts().get().sync(delegate(ActionInventoryGetProducts action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncGetProductList()
        {
            app().getProducts().get().async(delegate(ActionInventoryGetProducts action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncBuyProduct()
        {
			int quantity = 1;
            app().getProducts().buyItem(VALID_PRODUCT_ID,quantity).sync(delegate(ActionInventoryPurchase action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app ().getUser().getInventory()[VALID_PRODUCT_ID].getQuantity(),quantity);
            });
        }

        [Test]
        public void AsyncBuyProduct()
        {
            int quantity = 1;
            app().getProducts().buyItem(VALID_PRODUCT_ID,quantity).async(delegate(ActionInventoryPurchase action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app ().getUser().getInventory()[VALID_PRODUCT_ID].getQuantity(),quantity);
                Release();
            });

            Wait();
		}
		
	}
}