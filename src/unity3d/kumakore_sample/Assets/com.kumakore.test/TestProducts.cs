using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestProducts : TestBase
    {
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
            app().getProducts().buyItem(VALID_PRODUCT_ID,1).sync(delegate(ActionInventoryPurchase action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
        public void AsyncBuyProduct()
        {
            app().getProducts().buyItem(VALID_PRODUCT_ID,1).async(delegate(ActionInventoryPurchase action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
		}
		
	}
}