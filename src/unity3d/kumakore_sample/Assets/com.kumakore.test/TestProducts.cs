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
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
		}
		
		[Test]
        public void SyncGetProductList()
        {
            app().products().get().sync(delegate(ActionAppProductListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncGetProductList()
        {
            app().products().get().async(delegate(ActionAppProductListGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
		}
		
		[Test]
        public void SyncBuyProduct()
        {
            app().products().buyItem(VALID_PRODUCT_ID,1).sync(delegate(ActionAppBuyItem action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncBuyProduct()
        {
            app().products().buyItem(VALID_PRODUCT_ID,1).async(delegate(ActionAppBuyItem action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
		}
		
	}
}